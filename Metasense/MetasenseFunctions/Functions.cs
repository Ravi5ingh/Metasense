using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.Neural.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Lma;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Util.Simple;
using ExcelDna.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Metasense.Infrastructure;
using Metasense.Math;
using Metasense.MetasenseFunctions.Neural;


namespace Metasense.MetasenseFunctions
{
    /// <summary>
    /// Main class exposing all the functions
    /// </summary>
    public class Functions
    {

        /// <summary>
        /// This function loads a Table from a file location
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns></returns>
        [ExcelFunction(
            Name = "MTS_LoadTable",
            Description = "Loads a Table",
            Category = "Functions")]
        public static string LoadTable(
            [ExcelArgument(Name = "Name", Description = "The name of the table to be loaded")]
            string name,
            [ExcelArgument(Name = "Location", Description = "The path of the file")]
            string location,
            [ExcelArgument(Name = "Delimiter", Description = "The delimiting character (Default : ',')")]
            string delimiter)
        {
            var fileInfo = new FileInfo(location);

            Table table;
            switch (fileInfo.Extension.ToUpper())
            {
                case ".CSV":
                    var resolveDelimiter = Arg(delimiter, "Delimiter").AsChar(',');
                    table = Table.LoadFromCSV(fileInfo, resolveDelimiter);
                    break;
                default:
                    return ("File extension : " + fileInfo.Extension + " not recognized");
            }

            return ObjectStore.Add(name, table);
        }

        /// <summary>
        /// The Gaussian function
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns></returns>
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

        /// <summary>
        /// This function multiplies the input by 2
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns></returns>
        [ExcelFunction(
            Name = "MTS_Times2",
            Description = "Joins a string to a number",
            Category = "Functions")]
        public static double TimeTwo(double value)
        {
            return value * 2;
        }

        /// <summary>
        /// Gets the time
        /// </summary>
        /// <returns></returns>
        [ExcelFunction(
            Name = "MTS_GetTime",
            Description = "Gets the time",
            Category = "ML Functions")]
        public static object GetTime()
        {
            return null;
        }

        /// <summary>
        /// Gets the time
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Run a computation with a neural network
        /// </summary>
        /// <param name="networkXl">The network</param>
        /// <param name="inputXl">The inputs</param>
        /// <returns></returns>
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
