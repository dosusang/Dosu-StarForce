using System;

namespace StarForce {
    public enum OrderType  {
        NONE,
        GETINPUT,
        OUTPUT,
        COPYFROM,
        COPYTO,
        ADDTO,
        SUBTO,
        DIVTO,
        MULTO,
        JUMPTO,
        IFZEROJTO,
    };

    public enum InfoTypes {
        Error,
        Wram,
        Info,
    };

}