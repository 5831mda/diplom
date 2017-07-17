using System;
using System.Collections.Generic;

namespace PersonalSystemContol.Documentation
{
    /// <summary>
    /// Документация по API версии 1.
    /// </summary>
    public class V1Documentation
    {
        public  Dictionary<string,string> AvailableMethods
        {
            get
            {
                var dictionary = new Dictionary<string, string>
                                 {
                                     {
                                         "Documentation about API v1", "[GET]api/v1"
                                     },
                                     {
                                         "Add Engineer", "[POST:string fullName]api/v1/engineers"
                                     },
                                     {
                                         "Add Task", "[POST:Task task]api/v1/tasks"
                                     },
                                     {
                                         "Get tasks for an engineer",
                                         "[GET]api/v1/engineers/{id}/tasks"
                                     },
                                     {
                                         "Close Task", "[POST: Report]api/v1/tasks/{taskId}/finish"
                                     },
                                     {
                                         "Get tasks by period",
                                         "[GET:DateTime period_start,period_end]api/v1/tasks/period"
                                     },
                                     {
                                         "Submit a job for revision",
                                         "[POST]api/v1/tasks/{taskId}/remake"
                                     }
                                 };
                return dictionary;
            }
        }
    }
}
