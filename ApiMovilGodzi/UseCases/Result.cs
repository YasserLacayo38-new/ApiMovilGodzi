using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSuper_Aplication
{
    public class Result<T>
    {
        public T? Value { get; }
        public bool IsSuccess { get; }
        public string? Error { get; }

        private Result(T? value, bool isSuccess, string? error)
        { Value = value; IsSuccess = isSuccess; Error = error; }

        public static Result<T> Success(T value) => new Result<T>(value, true, null);
        public static Result<T> Failure(string? error) => new Result<T>(default, false, error);
    }
}
