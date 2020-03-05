using System.Collections.Generic;

namespace OBLRInstall
{
    public partial class MainWindow
    {
        private struct Result
        {
            public static Result Success { get; } = new Result();

            public bool IsSuccess => string.IsNullOrEmpty(error);
            public string error;

            public static implicit operator bool(Result result) => result.IsSuccess;

            public static bool operator ==(Result result, bool b) => result.IsSuccess == b;

            public static bool operator !=(Result result, bool b) => result.IsSuccess != b;

            public override bool Equals(object obj) => IsSuccess.Equals(obj) || base.Equals(obj);

            public override int GetHashCode() => EqualityComparer<string>.Default.GetHashCode(error);
        }

    }
}
