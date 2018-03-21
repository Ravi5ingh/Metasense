using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using ExcelDna.Integration;
using System;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;
using Metasense.Math;
using Metasense.MetasenseFunctions.DateTime;
using Metasense.MetasenseFunctions.ML;
using Metasense.MetasenseFunctions.Statistical;
using Metasense.MetasenseFunctions.Tabular;
using Metasense.Tabular;
using Microsoft.Office.Interop.Excel;


namespace Metasense.MetasenseFunctions
{
    /// <summary>
    /// Main class exposing all the functions
    /// </summary>
    public class Functions
    {
        [ExcelFunction(
            Name = "MTS_LoadTable",
            Description = "Loads a Table",
            Category = "Functions")]
        public static object LoadTable(
            [ExcelArgument(Name = "Name", Description = "The name of the table to be loaded")]
            string name,
            [ExcelArgument(Name = "Location", Description = "The path of the file")]
            string location,
            [ExcelArgument(Name = "Delimiter", Description = "The delimiting character (Default : ',')")]
            string delimiter)
        {
            var function = new LoadTable(Enums.FunctionType.Heavy | Enums.FunctionType.Sticky)
            {
                Name = Arg(name, "Table Name"),
                Location = Arg(location, "File location"),
                Delimiter = Arg(delimiter, "Delimiter")
            };

            return FunctionRunner.Run(function);
        }

        /// TODO : ths is shit. make it a function
        [ExcelFunction(
            Name = "MTS_ShowTable",
            Description = "Shows a table",
            Category = "Functions")]
        public static object ShowTable(
            string tableXl)
        {
            return Arg(tableXl, "Table").GetFromStoreAs<Table>().Data;
        }

        [ExcelFunction(
            Name = "MTS_LoadTimeSeries",
            Description = "Loads a Time Series from a table",
            Category = "Functions")]
        public static object LoadTimeSeries(
            [ExcelArgument(Name = "Name", Description = "The name to be given to the loaded time series")]
            object nameXl,
            [ExcelArgument(Name = "Table", Description = "The raw data table")]
            object tableXl,
            [ExcelArgument(Name = "Date Column Index", Description = "The index of the date column")]
            object dateColumnIndexXl,
            [ExcelArgument(Name = "Value Column Index", Description = "The index of the value column")]
            object valueColumnIndexXl)
        {
            var function = new LoadTimeSeries(Enums.FunctionType.Heavy | Enums.FunctionType.Sticky)
            {
                Name = Arg(nameXl, "Name"),
                DataTable = Arg(tableXl, "Data Table"),
                DateColumnIndex = Arg(dateColumnIndexXl, "Date Column Index"),
                ValueColumnIndex = Arg(valueColumnIndexXl, "Value Column Index")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_CreateSQLConnection",
            Description = "Creates a non-live SQL connection to a database",
            Category = "Functions")]
        public static object CreateSQLConnection(
            [ExcelArgument(Name = "Connection Name", Description = "The name of the connection object")]
            object nameXl,
            [ExcelArgument(Name = "Server Name", Description = "The name of the server")]
            object serverNameXl,
            [ExcelArgument(Name = "Database Name", Description = "The name of the database")]
            object databaseNameXl)
        {
            var function = new CreateSQLConnection(Enums.FunctionType.Light)
            {
                Name = Arg(nameXl, "Connection name"),
                ServerName = Arg(serverNameXl, "Server Name"),
                DatabaseName = Arg(databaseNameXl, "Database Name")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_ExecuteSQL",
            Description = "Executes an SQL query given an SQL connection and loads the results into a table",
            Category = "Functions")]
        public static object ExecuteSQLConnection(
            [ExcelArgument(Name = "Table Name", Description = "The name of the table to load")]
            object nameXl,
            [ExcelArgument(Name = "SQL Connection Object", Description = "An SQL connection object")]
            object sqlConnectionXl,
            [ExcelArgument(Name = "SQL Query", Description = "The string query to execute")]
            object sqlQueryXl)
        {
            var function = new ExecuteSQL(Enums.FunctionType.Heavy | Enums.FunctionType.Sticky)
            {
                Name = Arg(nameXl, "Table Name"),
                SQLConnection = Arg(sqlConnectionXl, "SQL Connection Object"),
                SQLQuery = Arg(sqlQueryXl, "SQL Query")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_ShowTimeSeries",
            Description = "Shows a loaded Time Series",
            Category = "Functions")]
        public static object ShowTimeSeries(
            [ExcelArgument(Name = "Time Series", Description = "The time series object to show")]
            object timeSeriesXl,
            [ExcelArgument(Name = "Show Headers", Description = "Wether or not to show headers (Default : True)")]
            object showHeadersXl)
        {
            var function = new ShowTimeSeries(Enums.FunctionType.Light)
            {
                TimeSeries = Arg(timeSeriesXl, "Time Series"),
                ShowHeaders = Arg(showHeadersXl, "Show Headers")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_CropTimeSeries",
            Description = "Crops the given time series and returns a new one",
            Category = "Functions")]
        public static object CropTimeSeries(
            [ExcelArgument(Name = "Name", Description = "The name to be given to the new time series")]
            object nameXl,
            [ExcelArgument(Name = "Input Time Series", Description = "The path of the file")]
            object inputTimeSeriesXl,
            [ExcelArgument(Name = "Start Time", Description = "The starting point to take")]
            object startTimeXl,
            [ExcelArgument(Name = "End Time", Description = "The end point to take")]
            object endTimeXl)
        {
            var function = new CropTimeSeries(Enums.FunctionType.Heavy)
            {
                Name = Arg(nameXl, "Name"),
                InputTimeSeries = Arg(inputTimeSeriesXl, "Input Time Series"),
                Start = Arg(startTimeXl, "Start Date"),
                End = Arg(endTimeXl, "End Date"),
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_BucketTimeSeries",
            Description = "Given an original time series, crops it to the given period, buckets the values into the given sizes and stores another object for it",
            Category = "Functions")]
        public static object BucketTimeSeries(
            [ExcelArgument(Name = "Name", Description = "The name to be given to the new time series")]
            object nameXl,
            [ExcelArgument(Name = "Input Time Series", Description = "The original time series object")]
            object inputTimeSeriesXl,
            [ExcelArgument(Name = "Start Time", Description = "The starting point to take")]
            object startTimeXl,
            [ExcelArgument(Name = "End Time", Description = "The end point to take")]
            object endTimeXl,
            [ExcelArgument(Name = "Bucket Size", Description = "The size of the intervals out of which the buckets are made. This is in seconds")]
            object bucketSizeXl)
        {
            var function = new BucketTimeSeries(Enums.FunctionType.Heavy)
            {
                Name = Arg(nameXl, "Name"),
                InputTimeSeries = Arg(inputTimeSeriesXl, "Input Time Series"),
                StartDateTime = Arg(startTimeXl, "Start Date"),
                EndDateTime = Arg(endTimeXl, "End Date"),
                IntervalBucketSize = Arg(bucketSizeXl, "Bucket Size")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_MergeTimeSeries",
            Description = "Merges the dates or both the dates and values of the given time series objects",
            Category = "Functions")]
        public static object MergeTimeSeries(
            [ExcelArgument(Name = "Input Time Series", Description = "A set of time series objects")]
            object inputTimeSeriesXl,
            [ExcelArgument(Name = "Perform Full Merge", Description = "Merges dates and value if true, only dates otherwise")]
            object performFullMergeXl)
        {
            var function = new MergeTimeSeries(Enums.FunctionType.Light)
            {
                InputTimeSeries = Arg(inputTimeSeriesXl, "Input Time Series"),
                FullMerge = Arg(performFullMergeXl, "Peform Full Merge")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_Gaussian",
            Description = "Performs the gaussian function on the inputs",
            Category = "Functions")]
        public static double Gaussian(
            [ExcelArgument(Name = "a", Description = "The leading co-efficient")]
            object a,
            [ExcelArgument(Name = "µ", Description = "The mean of the gaussian")]
            object µ,
            [ExcelArgument(Name = "σ", Description = "The standard devation of the gaussian")]
            object σ,
            [ExcelArgument(Name = "x", Description = "The input value")]
            object x)
        {
            var resolvedA = Arg(a, "Leading co-efficient").AsDouble();
            var resolvedMean = Arg(µ, "Mean").AsDouble();
            var resolvedStdDev = Arg(σ, "Standard Deviation").AsDouble();
            var resolvedX = Arg(x, "x Value").AsDouble();
            return MathFunctions.Gaussian(resolvedA, resolvedMean, resolvedStdDev, resolvedX);
        }

        [ExcelFunction(
            Name = "MTS_FitGaussian",
            Description = "Fits a Gaussian to a scatter plot given the x and y values. Returns the mean, standard deviation, and a value",
            Category = "Functions")]
        public static object FitGaussian(
            [ExcelArgument(Name = "x Values", Description = "The x co-ordinates of the scatter plot")]
            object xValuesXl,
            [ExcelArgument(Name = "y Values", Description = "The y co-ordinates of the scatter plot")]
            object yValuesXl,
            [ExcelArgument(Name = "Show Param Labels", Description = "Whether or not to include the parameter labels in the output (Default : False)")]
            object showLabelsXl)
        {
            var function = new FitGaussian(Enums.FunctionType.Light)
            {
                XValues = Arg(xValuesXl, "x Value"),
                YValues = Arg(yValuesXl, "y Values"),
                ShowLabels = Arg(showLabelsXl, "Show Labels")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_GetTimeOfDay",
            Description = "Gets the time of day for the given date(s). If the input is a table of dates, the output is also a table of dates and the output tabel name parameter is mandatory",
            Category = "Functions")]
        public static object GetTimeOfDay(
            [ExcelArgument(Name = "Dates", Description = "A single date or a table of dates as input")]
            object datesXl,
            [ExcelArgument(Name = "Output Table Nam", Description = "The name of the output tabe in case the input is a table")]
            object outputTableNameXl)
        {
            var function = new GetTimeOfDay(Enums.FunctionType.Light)
            {
                Dates = Arg(datesXl, "Input Date(s)"),
                OutputTableName = Arg(outputTableNameXl, "Output Table Name (Mandatory if table is provided)")
            };

            return FunctionRunner.Run(function);
        }


        [ExcelFunction(
            Name = "MTS_GetTSDates",
            Description = "Gets the dates from a time series object and returns a table of doubles representing the DateTime objects",
            Category = "Functions")]
        public static object GetTSDates(
            [ExcelArgument(Name = "Name", Description = "The name to give the output table")]
            object nameXl,
            [ExcelArgument(Name = "Time Series", Description = "A time series object")]
            object timeSeriesXl)
        {
            var function = new GetTSDates(Enums.FunctionType.Light)
            {
                Name = Arg(nameXl, "Name"),
                TimeSeries = Arg(timeSeriesXl, "Time Series")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_GetTSValues",
            Description = "Gets the values from a time series object and returns a table",
            Category = "Functions")]
        public static object GetTSValues(
            [ExcelArgument(Name = "Name", Description = "The name to give the output table")]
            object nameXl,
            [ExcelArgument(Name = "Time Series", Description = "A time series object")]
            object timeSeriesXl)
        {
            var function = new GetTSValues(Enums.FunctionType.Light)
            {
                Name = Arg(nameXl, "Name"),
                TimeSeries = Arg(timeSeriesXl, "Time Series")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_Unique",
            Description = "Gets the unique values in the given range",
            Category = "Functions")]
        public static object Unique(
            [ExcelArgument(Name = "Input Range", Description = "The range of values to get unique values from")]
            object inputRangeXl)
        {
            var function = new Unique(Enums.FunctionType.Light)
            {
                InputRange = Arg(inputRangeXl, "Input Range")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_Diff",
            Description = "Gets all values in input 1 that are not in input 2",
            Category = "Functions")]
        public static object Diff(
            [ExcelArgument(Name = "Input 1", Description = "The main set")]
            object input1Xl,
            [ExcelArgument(Name = "Input 2", Description = "The smaller subset of Input 1")]
            object input2Xl)
        {
            var function = new Diff(Enums.FunctionType.Light)
            {
                Input1 = Arg(input1Xl, "Input 1"),
                Input2 = Arg(input2Xl, "Input 2")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_CurrentPath",
            Description = "Get the path of the current workbook",
            Category = "Functions")]
        public static object GetCurrentWorkbookPath()
        {
            var app = ((Application) ExcelDnaUtil.Application);
            return $@"{app.ActiveWorkbook.Path}\{app.ActiveWorkbook.FullName}";
        }

        [ExcelFunction(
            Name = "MTS_Times2",
            Description = "Joins a string to a number",
            Category = "Functions")]
        public static double TimeTwo(double value)
        {
            return value * 2;
        }

        [ExcelFunction(
            Name = "MTS_Normalize",
            Description = "Normalizes the input data",
            Category = "Functions")]
        public static object Normalize(
            [ExcelArgument(Name = "Inputs", Description = "The inputs to be normalized")]
            object inputsXl)
        {
            var function = new Normalize(Enums.FunctionType.Light)
            {
                Inputs = Arg(inputsXl, "Inputs")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_TrainNetwork",
            Description = "Trains a neural network and stores it in memory",
            Category = "ML Functions")]
        public static object TrainNetwork(
            [ExcelArgument(Name = "Name", Description = "The name of the neural network to be built")]
            object nameXl,
            [ExcelArgument(Name = "Input Activation Function", Description = "The activation function for the input layer")]
            object inputActivationFuncXl,
            [ExcelArgument(Name = "Input Layer has bias?", Description = "Whether or not the input layer has a bias")]
            object inputHasBiasXl,
            [ExcelArgument(Name = "Hidden Layer Config", Description = "A structured range detailing the network configuration")]
            object hiddenConfigXl,
            [ExcelArgument(Name = "Output Activation Function", Description = "The activation function for the output layer")]
            object outputActivationFuncXl,
            [ExcelArgument(Name = "Output Layer has bias?", Description = "Whether or not the output layer has a bias")]
            object outputHasBiasXl,
            [ExcelArgument(Name = "Inputs", Description = "The traning data inputs")]
            object inputsXl,
            [ExcelArgument(Name = "Targets", Description = "The training data target")]
            object targetsXl,
            [ExcelArgument(Name = "Error Tolerance", Description = "The error tolerance to train within")]
            object errorToleranceXl,
            [ExcelArgument(Name = "Epoch Cut-off", Description = "The maximum number of epoch to train for")]
            object epochLimitXl)
        {
            var function = new TrainNetwork(Enums.FunctionType.Heavy | Enums.FunctionType.Sticky)
            {
                Name = Arg(nameXl, "Name"),
                InputActivationFunction = Arg(inputActivationFuncXl, "Input Activation Function"),
                InputHasBias = Arg(inputHasBiasXl, "Input Has Bias"),
                HiddenLayerConfig = Arg(hiddenConfigXl, "Hidden Layer Configuration"),
                OutputActivationFunction = Arg(outputActivationFuncXl, "Output Activation Function"),
                OutputHasBias = Arg(outputHasBiasXl, "Output Bias Flag"),
                Inputs = Arg(inputsXl, "Input Values"),
                Targets = Arg(targetsXl, "Target Values"),
                ErrorTolerance = Arg(errorToleranceXl, "Error Tolerance"),
                EpochLimit = Arg(epochLimitXl, "Epoch Limit")
            };

            return FunctionRunner.Run(function);
        }

        [ExcelFunction(
            Name = "MTS_ComputeWithNetwork",
            Description = "Performs a computation with the given neural network",
            Category = "ML Functions")]
        public static object ComputeWithNetwork(
            [ExcelArgument(Name = "Network", Description = "The trained neural network")]
            object networkXl,
            [ExcelArgument(Name = "Input", Description = "The input on which to perform the computation")]
            object inputXl)
        {
            if (!ExcelDnaUtil.IsInFunctionWizard())
            {
                try
                {
                    //Parameter resolution
                    var network = Arg(networkXl, "Network").GetFromStoreAs<BasicNetwork>();
                    var inputs = Arg(inputXl, "Inputs").As1DArray<double>();

                    //Computation and return
                    var result = network.Compute(new BasicMLData(inputs));
                    var retVal = new double[result.Count];

                    result.CopyTo(retVal, 0, result.Count);

                    return retVal;
                }
                catch (Exception exp)
                {
                    return "#Err - " + exp.Message;
                }
            }
            else
            {
                return "...";
            }
        }

        /// <summary>
        /// Create an ExcelArg object
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static ExcelArg Arg(object argument, string name)
        {
            return new ExcelArg(argument, name);
        }
    }
}
