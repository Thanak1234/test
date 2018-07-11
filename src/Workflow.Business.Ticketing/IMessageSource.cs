/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;

namespace Workflow.Business.Ticketing
{
    public interface IMessageSource
    {
        List<string> getTo();
        List<string> getCc();
        List<String> getBcc();
        string getSubject();
        string getBodyHeader();
        string getMainContent();
        string getSignature();
        object getData();
    }
}
