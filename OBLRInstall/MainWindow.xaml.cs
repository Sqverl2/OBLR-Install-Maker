using Newtonsoft.Json;
using OBLRInstall.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OBLRInstall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string CONFIGURATION_FILE_NAME = "Config.json";

        private PackagesConfiguration configuration;

        public static readonly DependencyProperty IsInputEnabledProperty = DependencyProperty.Register("IsInputEnabled", typeof(bool), typeof(MainWindow));

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("SourcePath", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnSourcePathChanged)));

        private static void OnSourcePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserSettings.Default.SourcePath = (string)e.NewValue;
            UserSettings.Default.Save();

            var window = (MainWindow)d;
            // This is our UI thread so we don't want to block it. Executing refresh fire and forget style.
            Task.Run(async () => await window.PerformLongAction(window.RefreshStates)).ConfigureAwait(false);
        }

        public static readonly DependencyProperty DestinationProperty = DependencyProperty.Register("DestinationPath", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnDestinationPathChanged)));

        private static void OnDestinationPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserSettings.Default.DestinationPath = (string)e.NewValue;
            UserSettings.Default.Save();
        }

        public bool IsInputEnabled
        {
            set => Dispatcher.Invoke(() => SetValue(IsInputEnabledProperty, value));
        }

        public string SourcePath
        {
            get => UserSettings.Default.SourcePath;
            set
            {
                UserSettings.Default.SourcePath = value;
                Dispatcher.Invoke(() => SetValue(SourceProperty, value));
            }
        }

        public string DestinationPath
        {
            get => UserSettings.Default.DestinationPath;
            set
            {
                UserSettings.Default.DestinationPath = value;
                Dispatcher.Invoke(() => SetValue(DestinationProperty, value));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            var applicationPath = AppDomain.CurrentDomain.BaseDirectory;

#if DEBUG
            //UserSettings.Default.Reset();
            CreateDefaultConfig(applicationPath);
#endif

            var result = ReadConfig(applicationPath);
            if (result) result = InitSourcePath(applicationPath);
            if (result) result = InitDestinationPath(applicationPath);
            if (result) result = InitStages();
            // This is our UI thread so we don't want to block it. Executing refresh fire and forget style.
            if (result) Task.Run(async () => await PerformLongAction(RefreshStates)).ConfigureAwait(false);
            if (!result) ShowError(result.error);
        }

        private void CreateDefaultConfig(string applicationPath)
        {
            var configPath = Path.Combine(applicationPath, CONFIGURATION_FILE_NAME);
            File.WriteAllText(configPath, JsonConvert.SerializeObject(new DefaultConfiguration()));
        }

        private Result ReadConfig(string applicationPath)
        {
            try
            {
                var configPath = Path.Combine(applicationPath, CONFIGURATION_FILE_NAME);
                configuration = JsonConvert.DeserializeObject<PackagesConfiguration>(File.ReadAllText(configPath));
                return Result.Success;
            }
            catch
            {
#if DEBUG
                throw;
#else
                return new Result { error = "Failed to read or parse configuration." };
#endif
            }
        }

        private Result InitSourcePath(string basePath)
        {
            try
            {
                var settingsSourcePath = UserSettings.Default.SourcePath;
                SourcePath = string.IsNullOrWhiteSpace(settingsSourcePath) ? Path.Combine(basePath, $"OBLR v{configuration.Version}", "OBLR 7zips") : settingsSourcePath;
                return Result.Success;
            }
            catch
            {
                SourcePath = string.Empty;
                return Result.Success;
            }
        }

        private Result InitDestinationPath(string basePath)
        {
            try
            {
                var settingDestinationPath = UserSettings.Default.DestinationPath;
                DestinationPath = string.IsNullOrWhiteSpace(settingDestinationPath) ? Path.Combine(basePath, $"OBLR v{configuration.Version}", "OBLR Installer") : settingDestinationPath;
                return Result.Success;
            }
            catch
            {
                SourcePath = string.Empty;
                return Result.Success;
            }
        }

        private Result InitStages()
        {
            try
            {
                var stagesContainer = StagesContainer;
                stagesContainer.ItemsSource = configuration.Stages;
                return Result.Success;
            }
            catch
            {
#if DEBUG
                throw;
#else
                return new Result { error = "Failed to create program layout. Check configuration file." };
#endif
            }
        }

        private async Task<Result> RefreshStates()
        {
            try
            {
                if (!Directory.Exists(SourcePath)) return new Result { error = "Source dirrectory not found." };
                await PerformLongActionForEachStep(RefreshStepState);
                return Result.Success;
            }
            catch
            {
#if DEBUG
                throw;
#else
                return new Result { error = "Could not verify current file states." };
#endif
            }
        }

        private void RefreshStepState(int stageIndex, InstallationStep step)
        {
            if (string.IsNullOrWhiteSpace(step.FileName)) return;
            var state = step.Dispatcher.Invoke(() => step.CurrentState);
            if (state == InstallationStep.State.FAILED) return;
            if (state == InstallationStep.State.COMPLETED) return;

            // File is in correct stage folder.
            var stepFolder = step.SuggestedSourcePath;
            if (string.IsNullOrWhiteSpace(stepFolder)) stepFolder = stageIndex.ToString();
            var correctDirrectoryPath = Path.Combine(SourcePath, stepFolder);
            if (File.Exists(Path.Combine(correctDirrectoryPath, step.FileName)))
            {
                state = InstallationStep.State.READY;
            }
            // File is in wrong folder.
            else if (Directory.GetFiles(SourcePath, step.FileName, SearchOption.AllDirectories).Any())
            {
                state = InstallationStep.State.WRONG_LOCATION;
            }
            // File is missing
            else
            {
                state = InstallationStep.State.MISSING;
            }

            step.Dispatcher.InvokeAsync(() => step.CurrentState = state);
        }

        private async Task<Result> FixLocations()
        {
            try
            {
                if (!Directory.Exists(SourcePath)) return new Result { error = "Source dirrectory not found." };

                await PerformLongActionForEachStep(FixStepLocation);
                return Result.Success;
            }
            catch
            {
#if DEBUG
                throw;
#else
                return new Result { error = "Failed to relocate one or more files." };
#endif
            }
        }

        private void FixStepLocation(int stageIndex, InstallationStep step)
        {
            if (string.IsNullOrWhiteSpace(step.FileName)) return;

            var stepFolder = step.SuggestedSourcePath;
            if (string.IsNullOrWhiteSpace(stepFolder)) stepFolder = stageIndex.ToString();
            var correctDirrectoryPath = Path.Combine(SourcePath, stepFolder);
            var correctFilePath = Path.Combine(correctDirrectoryPath, step.FileName);

            // File is already in correct stage folder.
            if (File.Exists(correctFilePath)) return;

            var currentFilePath = Directory.GetFiles(SourcePath, step.FileName, SearchOption.AllDirectories).FirstOrDefault();
            if (!File.Exists(currentFilePath))
            {
                step.Dispatcher.InvokeAsync(() => step.CurrentState = InstallationStep.State.MISSING);
                return;
            }

            if (!Directory.Exists(correctDirrectoryPath))
            {
                Directory.CreateDirectory(correctDirrectoryPath);
            }

            File.Move(currentFilePath, correctFilePath);
            step.Dispatcher.InvokeAsync(() => step.CurrentState = InstallationStep.State.READY);
        }

        private async Task<Result> Unpack()
        {
            try
            {
                if (!Directory.Exists(SourcePath)) return new Result { error = "Source dirrectory not found." };

                await PerformLongActionForEachStage(ExecuteStageSteps);
                return Result.Success;
            }
            catch
            {
#if DEBUG
                throw;
#else
                return new Result { error = "Failed to unpack one or more files." };
#endif
            }
        }

        private async Task ExecuteStageSteps(int stageIndex, InstallationStage stage)
        {
            foreach (var step in stage.Steps)
            {
                try
                {
                    ExecuteStep(stageIndex, step);
                    await step.Dispatcher.InvokeAsync(() => step.CurrentState = InstallationStep.State.COMPLETED);
                }
                catch
                {
                    await step.Dispatcher.InvokeAsync(() => step.CurrentState = InstallationStep.State.FAILED);
#if DEBUG
                    throw;
#else
                    break;
#endif
                }
            }
        }

        private void ExecuteStep(int stageIndex, InstallationStep step)
        {
            switch (step.Operation)
            {
                case StepType.UNPACK:
                    ExecuteUnpackStep(stageIndex, step);
                    break;
                case StepType.RUN:
                    ExecuteRunStep(stageIndex, step);
                    break;
                case StepType.COPY:
                    ExecuteCopyStep(stageIndex, step);
                    break;
                default:
                    throw new NotSupportedException("Operation not supported.");
            }
        }

        private void ExecuteUnpackStep(int stageIndex, InstallationStep step)
        {
            var stepFileName = step.FileName;
            var stepDestinationFolder = step.DestinationFolder;
            var stepInclude = step.Include;
            var stepIgnore = step.Ignore;

            var sourceFilePath = FindSourceFilePath(stageIndex, step);
            
            var destinationFolderPath = Path.Combine(DestinationPath, stageIndex.ToString(), stepDestinationFolder ?? string.Empty);

            using (var extractor = new SevenZipExtractor.ArchiveFile(sourceFilePath))
            {
                extractor.Extract((entry) =>
                {
                    if (entry.IsFolder) return null;

                    var entryPath = NormalizePath(entry.FileName);
                    
                    bool include = IsPathMatchingRules(entryPath, stepInclude, out var resultPath);
                    if (include) include = !CheckIsIgnored(entryPath, stepIgnore);
                    if (!include) return null;

                    var filePath = Path.Combine(destinationFolderPath, resultPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    return filePath;
                });
            }
        }

        private void ExecuteRunStep(int stageIndex, InstallationStep step)
        {
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(step.FileName))
                {
                    var stepFolder = step.SuggestedSourcePath;
                    if (string.IsNullOrWhiteSpace(stepFolder)) stepFolder = stageIndex.ToString();
                    // NB! This is strange, but makes logical sense for now. Installations are run from destination folder, not source.
                    // This is due to some installation being inside archives.
                    // If installation is not in archive, and you need to run it, consider writing copy operation step before run.
                    var path = Path.Combine(DestinationPath, stepFolder, step.FileName);
                    try
                    {
                        Process.Start(path);
                    }
                    catch
                    {
                        /* 
                         * Ignoring Win32 exceptions. 
                         * We can't exactly distinguish which one was thrown
                         * (other than doing extra bad practice of checking exception text.)
                         * But if it were possible I'd like to know if user just denied UAC.
                         */
                    }
                }

                Dispatcher.Invoke(() => MessageBox.Show(this, step.Description, step.Name, MessageBoxButton.OK, MessageBoxImage.Exclamation));

                var destinationPath = DestinationPath;
                if (!string.IsNullOrWhiteSpace(step.DestinationFolder))
                    destinationPath = Path.Combine(destinationPath, stageIndex.ToString(), step.DestinationFolder);

                if (step.Include.All(file => File.Exists(Path.Combine(destinationPath, file)))) break;
            }
        }

        private void ExecuteCopyStep(int stageIndex, InstallationStep step)
        {
            var sourcePath = FindSourceFilePath(stageIndex, step);
            var destinationPath = Path.Combine(DestinationPath, stageIndex.ToString(), step.FileName);
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, destinationPath);
            }
            // This check is not stricly necessary with current implementation, but who knows.
            else if (Directory.Exists(sourcePath))
            {
                DirectoryUtils.Copy(sourcePath, destinationPath);
            }
        }

        private bool IsPathMatchingRules(string entryPath, List<string> rules, out string remappedEntryPath)
        {
            const char maskChar = '*';
            const string pathSeparator = @"\";
            remappedEntryPath = entryPath;

            if (!rules.Any()) return true;

            foreach (var include in rules)
            {
                remappedEntryPath = entryPath;
                if (include.Contains(maskChar))
                {
                    var includeParts = include.Split(maskChar);

                    var includeFolder = includeParts.First();
                    // If no path before mask, then any path will do.
                    var folderMatches = string.IsNullOrWhiteSpace(includeFolder);
                    // If path before mask is root (single '\'), then only paths in root(not containing path separator '\') will do
                    if (!folderMatches) folderMatches = includeFolder.Equals(pathSeparator, StringComparison.InvariantCultureIgnoreCase) && !entryPath.Contains(pathSeparator);
                    // If target path start with path before mask, it is contained.
                    if (!folderMatches)
                    {
                        folderMatches = entryPath.StartsWith(includeFolder, StringComparison.InvariantCultureIgnoreCase);
                        // Since this is a case with mask, we remove folder from path.
                        if (folderMatches) remappedEntryPath = entryPath.Substring(includeFolder.Length);
                    }
                    // No folder rule matched.
                    if (!folderMatches) continue;

                    var includeExtention = includeParts.Length > 1 ? includeParts.Last() : null;
                    // If no exention after mask, any extention will do.
                    var extentionMatches = string.IsNullOrWhiteSpace(includeExtention);
                    // If there is an extention after mask, validate it.
                    if (!extentionMatches) extentionMatches = entryPath.EndsWith(includeExtention, StringComparison.InvariantCultureIgnoreCase);
                    // No extention rule matched.
                    if (!extentionMatches) continue;

                    return true;
                }
                else if (entryPath.StartsWith(include, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckIsIgnored(string entryName, List<string> rules)
        {
            if (!rules.Any()) return false;
            return IsPathMatchingRules(entryName, rules, out var ignored);
        }

        private string FindSourceFilePath(int stageIndex, InstallationStep step)
        {
            var stepFolder = step.SuggestedSourcePath;
            if (string.IsNullOrWhiteSpace(stepFolder)) stepFolder = stageIndex.ToString();
            var path = Path.Combine(SourcePath, stepFolder, step.FileName);
            // File is in default location.
            if (File.Exists(path))
            {
                return path;
            }

            // File is in wrong folder.
            path = Directory.GetFiles(SourcePath, step.FileName, SearchOption.AllDirectories).FirstOrDefault();
            if (!string.IsNullOrEmpty(path))
            {
                return path;
            }

            throw new FileNotFoundException("File not found: " + step.FileName);
        }

        private void ShowError(string error)
        {
            try
            {
                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                Application.Current.Shutdown();
            }
        }

        private async void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            await PerformLongAction(RefreshStates);
        }

        private async void OnFixLocationsButtonClick(object sender, RoutedEventArgs e)
        {
            await PerformLongAction(FixLocations);
        }

        private async void OnUnpackButtonClick(object sender, RoutedEventArgs e)
        {
            await PerformLongAction(Unpack);
        }


        private async Task PerformLongAction(Func<Task<Result>> action)
        {
            IsInputEnabled = false;
            var result = await action.Invoke();
            if (!result) ShowError(result.error);
            IsInputEnabled = true;
        }

        private async Task PerformLongActionForEachStep(Action<int, InstallationStep> action)
        {
            var tasks = new List<Task>();
            var stages = configuration.Stages;
            for (int stageIndex = 0; stageIndex < stages.Count; stageIndex++)
            {
                foreach (var step in stages[stageIndex].Steps)
                {
                    // Cached variables to avoid modifying closure variables.
                    int cachedIndex = stageIndex;
                    var cachedStep = step;
                    tasks.Add(Task.Run(() => action(cachedIndex, cachedStep)));
                }
            }

            await Task.WhenAll(tasks);
        }

        private async Task PerformLongActionForEachStage(Func<int, InstallationStage, Task> action)
        {
            var tasks = new List<Task>();
            var stages = configuration.Stages;
            for (int stageIndex = 0; stageIndex < stages.Count; stageIndex++)
            {
                int cachedIndex = stageIndex;
                var cachedStage = stages[stageIndex];
                tasks.Add(Task.Run(async () => await action(cachedIndex, cachedStage)));
            }

            await Task.WhenAll(tasks);
        }
        
        private static string NormalizePath(string path)
        {
            // Some archives have reverse slashes for separators. We are not on Linux, so f* those...
            // But actually, this normalization is not for file system.
            // So getting separator from Path is not correct. 
            // The purpose of this normalization is to match include/ignore rules.
            return path.Replace(@"/", @"\");
        }

        private IEnumerable<ArchiveEntryWrapper> OpenSevenZipExtractor(string filePath)
        {
            using (var extractor = new SevenZipExtractor.ArchiveFile(filePath))
            {
                var tasks = new List<Task>();
                foreach (var entry in extractor.Entries)
                {
                    if (entry.IsFolder) continue;
                    yield return new ArchiveEntryWrapper
                    {
                        filePath = entry.FileName,
                        writeToFile = (path) => tasks.Add(Task.Run(() =>
                        {
                            entry.Extract(path);
                        })),
                    };
                }
                Task.WhenAll(tasks).Wait();
            }
        }

        private struct ArchiveEntryWrapper
        {
            public string filePath;
            public Action<string> writeToFile;
        }
    }
}
