﻿using FluentValidation.Results;

namespace AnthonyTravelPortal.Domain.ResponseResult
{
    public interface IBaseResult
    {
        ValidationResult ValidationResult { get; set; }
        string Message { get; set; }
        bool Succeeded { get; set; }
    }

    public interface IBaseResult<out T> : IBaseResult
    {
        T Data { get; }
    }
}