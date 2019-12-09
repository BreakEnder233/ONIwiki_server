using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact
{
    public enum WikiResponseStatus
    {
        TIMEOUT = 0,

        NOTEXIST = 100,

        SUCCESS = 200,
        CREATE_NEW_RECORD = 201,

        FAIL = 500,
        DATA_DISTORTED = 501,
        PASSWORD_UNMATCH = 502
    }
}
