﻿using BLL.Interfaces;
using System.Linq;
using System.Net;

namespace BLL.Infrastructure.Extentions
{
    public static class AppActionResultExtention
    {
        public static IAppActionResult SetStatus(this IAppActionResult appActionResult, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (appActionResult.ErrorMessages.Count != 0)
                appActionResult.Status = (int)statusCodeIsError;
            else
                appActionResult.Status = (int)statusCodeIsSuccess;
            return appActionResult;
        }
        public static IAppActionResult AddErrors(this IAppActionResult destActionResult, IAppActionResult sourceActionResult)
        {
            var destErrors = destActionResult.ErrorMessages.ToList();
            var srcErrors = sourceActionResult.ErrorMessages.ToList();
            destErrors.AddRange(srcErrors);
            destActionResult.ErrorMessages = destErrors;
            return destActionResult;
        }

        public static IAppActionResult SetResult(this IAppActionResult destResult, IAppActionResult sourceResult)
        {
            destResult.ErrorMessages = sourceResult.ErrorMessages;
            destResult.Status = sourceResult.Status;
            return destResult;
        }
    }
}
