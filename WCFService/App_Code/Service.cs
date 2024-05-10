using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    DemoTable get(int var1, int var2)
    {
        return new DemoTable();
        ///somethiing
    }

    DemoTable IService.get(int var1, int var2)
    {
        throw new NotImplementedException();
    }
}
