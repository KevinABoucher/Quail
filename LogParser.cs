using System;
using System.Data;
using LogQuery = MSUtil.LogQueryClassClass;
using EventLogInputFormat = MSUtil.COMEventLogInputContextClassClass;
using FileSystemInputFormat = MSUtil.COMFileSystemInputContextClassClass;
using LogRecordSet = MSUtil.ILogRecordset;

namespace Quail
{
    /// <summary>
    /// Calls to LogParser.
    /// References:  http://www.logparser.com (See code by:  Edward Tewiah)
    /// </summary>
    public class LogParser
    {
        public LogParser()
        {
        }

        public static DataTable GetData(string queryST, ref string retErr)
        {

            DataTable EventDT = new DataTable("EventDT");

            try
            {
                // Instantiate the LogQuery object
                LogQuery oLogQuery = new LogQuery();

                // Instantiate the Event Log Input Format object
                EventLogInputFormat oEVTInputFormat = new EventLogInputFormat();

                // Set its "direction" parameter to "BW"
                oEVTInputFormat.direction = "BW";

                // Execute the query
                LogRecordSet oRecordSet = oLogQuery.Execute(queryST, oEVTInputFormat);

                int i = 0;

                for (; i < oRecordSet.getColumnCount(); i++)
                {
                    string colnm;
                    colnm = oRecordSet.getColumnName(i);

                    EventDT.Columns.Add(new DataColumn(colnm, typeof(string)));
                }

                for (; !oRecordSet.atEnd(); oRecordSet.moveNext())
                {
                    MSUtil.ILogRecord rowLP = null;
                    rowLP = oRecordSet.getRecord();

                    // populate holding table with site name & summary bytes for the period
                    DataRow dr = EventDT.NewRow();
                    for (int ct = 0; ct < i; ct++)
                    {
                        dr[ct] = rowLP.getValue(ct);
                    }
                    EventDT.Rows.Add(dr);
                }


                // Close the recordset
                oRecordSet.close();
            }
            catch (Exception exc)
            {
                retErr = "Unexpected error: " + exc.Message;
            }

            return EventDT;
        }


        public static DataTable FSquery(string queryST, ref string retErr)
        {
            DataTable resultsDT = new DataTable("resultsDT");
            try
            {
                // Instantiate the LogQuery object
                LogQuery oLogQuery = new LogQuery();

                // Instantiate the File System Input Format object
                FileSystemInputFormat oFSInputFormat = new MSUtil.COMFileSystemInputContextClassClass();

                // Execute the query
                LogRecordSet oRecordSet = oLogQuery.Execute(queryST, oFSInputFormat);
                int i = 0;
                for (; i < oRecordSet.getColumnCount(); i++)
                {
                    string colnm;
                    colnm = oRecordSet.getColumnName(i);

                    resultsDT.Columns.Add(new DataColumn(colnm, typeof(string)));

                }

                for (; !oRecordSet.atEnd(); oRecordSet.moveNext())
                {
                    MSUtil.ILogRecord rowLP = null;
                    rowLP = oRecordSet.getRecord();

                    DataRow dr = resultsDT.NewRow();
                    for (int ct = 0; ct < i; ct++)
                    {
                        dr[ct] = rowLP.getValue(ct);
                    }
                    resultsDT.Rows.Add(dr);
                }

                // Close the recordset
                oRecordSet.close();
            }
            catch (Exception exc)
            {
                retErr = "Unexpected error: " + exc.Message;
            }

            return resultsDT;
        }


        public static DataTable REGquery(string queryST, ref string retErr)
        {
            DataTable resultsDT = new DataTable("resultsDT");
            try
            {
                // Instantiate the LogQuery object
                LogQuery oLogQuery = new LogQuery();

                // Instantiate the File System Input Format object
                MSUtil.COMRegistryInputContextClassClass oREGInputFormat = new MSUtil.COMRegistryInputContextClassClass();

                // Execute the query
                LogRecordSet oRecordSet = oLogQuery.Execute(queryST, oREGInputFormat);
                int i = 0;
                for (; i < oRecordSet.getColumnCount(); i++)
                {
                    string colnm;
                    colnm = oRecordSet.getColumnName(i);

                    resultsDT.Columns.Add(new DataColumn(colnm, typeof(string)));

                }

                for (; !oRecordSet.atEnd(); oRecordSet.moveNext())
                {
                    MSUtil.ILogRecord rowLP = null;
                    rowLP = oRecordSet.getRecord();

                    DataRow dr = resultsDT.NewRow();
                    for (int ct = 0; ct < i; ct++)
                    {
                        dr[ct] = rowLP.getValue(ct);
                    }
                    resultsDT.Rows.Add(dr);
                }

                // Close the recordset
                oRecordSet.close();
            }
            catch (Exception exc)
            {
                retErr = "Unexpected error: " + exc.Message;
            }

            return resultsDT;
        }


        public static DataTable IISquery(string queryST, ref string retErr)
        {
            DataTable resultsDT = new DataTable("resultsDT");
            try
            {
                LogQuery oLogQuery = new LogQuery();
                MSUtil.COMIISW3CInputContextClassClass oInputFormat = new MSUtil.COMIISW3CInputContextClassClass();

                // Execute the query
                LogRecordSet oRecordSet = oLogQuery.Execute(queryST, oInputFormat);
                int i = 0;
                for (; i < oRecordSet.getColumnCount(); i++)
                {
                    string colnm;
                    colnm = oRecordSet.getColumnName(i);
                    resultsDT.Columns.Add(new DataColumn(colnm, typeof(string)));
                }

                for (; !oRecordSet.atEnd(); oRecordSet.moveNext())
                {
                    MSUtil.ILogRecord rowLP = null;
                    rowLP = oRecordSet.getRecord();

                    DataRow dr = resultsDT.NewRow();
                    for (int ct = 0; ct < i; ct++)
                    {
                        dr[ct] = rowLP.getValue(ct);
                    }
                    resultsDT.Rows.Add(dr);
                }

                // Close the recordset
                oRecordSet.close();
            }
            catch (Exception exc)
            {
                retErr = "Unexpected error: " + exc.Message;
            }

            return resultsDT;
        }

        public static DataTable W3Cquery(string queryST, ref string retErr)
        {
            DataTable resultsDT = new DataTable("resultsDT");
            try
            {
                LogQuery oLogQuery = new LogQuery();
                MSUtil.COMIISW3CInputContextClassClass oInputFormat = new MSUtil.COMIISW3CInputContextClassClass();

                // Execute the query
                LogRecordSet oRecordSet = oLogQuery.Execute(queryST, oInputFormat);
                int i = 0;
                for (; i < oRecordSet.getColumnCount(); i++)
                {
                    string colnm;
                    colnm = oRecordSet.getColumnName(i);
                    resultsDT.Columns.Add(new DataColumn(colnm, typeof(string)));
                }

                for (; !oRecordSet.atEnd(); oRecordSet.moveNext())
                {
                    MSUtil.ILogRecord rowLP = null;
                    rowLP = oRecordSet.getRecord();

                    DataRow dr = resultsDT.NewRow();
                    for (int ct = 0; ct < i; ct++)
                    {
                        dr[ct] = rowLP.getValue(ct);
                    }
                    resultsDT.Rows.Add(dr);
                }

                // Close the recordset
                oRecordSet.close();
            }
            catch (Exception exc)
            {
                retErr = "Unexpected error: " + exc.Message;
            }

            return resultsDT;
        }

        public static DataTable CSVquery(string queryST, ref string retErr)
        {
            DataTable resultsDT = new DataTable("resultsDT");
            try
            {
                LogQuery oLogQuery = new LogQuery();
                MSUtil.COMCSVInputContextClassClass oInputFormat = new MSUtil.COMCSVInputContextClassClass();

                // Execute the query
                LogRecordSet oRecordSet = oLogQuery.Execute(queryST, oInputFormat);
                int i = 0;
                for (; i < oRecordSet.getColumnCount(); i++)
                {
                    string colnm;
                    colnm = oRecordSet.getColumnName(i);
                    resultsDT.Columns.Add(new DataColumn(colnm, typeof(string)));
                }

                for (; !oRecordSet.atEnd(); oRecordSet.moveNext())
                {
                    MSUtil.ILogRecord rowLP = null;
                    rowLP = oRecordSet.getRecord();

                    DataRow dr = resultsDT.NewRow();
                    for (int ct = 0; ct < i; ct++)
                    {
                        dr[ct] = rowLP.getValue(ct);
                    }
                    resultsDT.Rows.Add(dr);
                }

                // Close the recordset
                oRecordSet.close();
            }
            catch (Exception exc)
            {
                retErr = "Unexpected error: " + exc.Message;
            }

            return resultsDT;
        }

        public static DataTable Textquery(string queryST, ref string retErr)
        {
            DataTable resultsDT = new DataTable("resultsDT");
            try
            {
                LogQuery oLogQuery = new LogQuery();
                MSUtil.COMTextLineInputContextClassClass oInputFormat = new MSUtil.COMTextLineInputContextClassClass();

                // Execute the query
                LogRecordSet oRecordSet = oLogQuery.Execute(queryST, oInputFormat);
                int i = 0;
                for (; i < oRecordSet.getColumnCount(); i++)
                {
                    string colnm;
                    colnm = oRecordSet.getColumnName(i);
                    resultsDT.Columns.Add(new DataColumn(colnm, typeof(string)));
                }

                for (; !oRecordSet.atEnd(); oRecordSet.moveNext())
                {
                    MSUtil.ILogRecord rowLP = null;
                    rowLP = oRecordSet.getRecord();

                    DataRow dr = resultsDT.NewRow();
                    for (int ct = 0; ct < i; ct++)
                    {
                        dr[ct] = rowLP.getValue(ct);
                    }
                    resultsDT.Rows.Add(dr);
                }

                // Close the recordset
                oRecordSet.close();
            }
            catch (Exception exc)
            {
                retErr = "Unexpected error: " + exc.Message;
            }

            return resultsDT;
        }

    }
}
