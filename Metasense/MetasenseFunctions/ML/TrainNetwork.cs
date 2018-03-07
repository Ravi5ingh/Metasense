using System;
using Encog.Engine.Network.Activation;
using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Lma;
using Metasense.Infrastructure;
using Metasense.Infrastructure.Functions;

namespace Metasense.MetasenseFunctions.ML
{
    /// <summary>
    /// This class implements MTS_TrainNetwork
    /// </summary>
    public class TrainNetwork : BaseFunction<BasicNetwork>
    {
        #region Parameters

        public ExcelArg Name { get; set; }

        public ExcelArg InputActivationFunction { get; set; }

        public ExcelArg InputHasBias { get; set; }

        public ExcelArg HiddenLayerConfig { get; set; }

        public ExcelArg OutputActivationFunction { get; set; }

        public ExcelArg OutputHasBias { get; set; }

        public ExcelArg Inputs { get; set; }

        public ExcelArg Targets { get; set; }

        public ExcelArg ErrorTolerance { get; set; }

        public ExcelArg EpochLimit { get; set; }

        #endregion

        #region Resolved Parameters

        private string name;

        private IActivationFunction inputActivationFunction;

        private bool inputHasBias;

        private object[,] hiddenLayerConfig;

        private IActivationFunction outputActivationFunction;

        private bool outputHasBias;

        private double[,] inputs;

        private double[,] targets;

        private double errorTolerance;

        private int epochLimit;

        #endregion

        /// <inheritdoc />
        public override void ResolveInputs()
        {
            name = Name.AsString();

            inputActivationFunction = Util.GetActivationFunction(InputActivationFunction.AsString("LINEAR"));

            inputHasBias = InputHasBias.AsBoolean(true);

            hiddenLayerConfig = HiddenLayerConfig.As2DArray<object>();

            outputActivationFunction = Util.GetActivationFunction(OutputActivationFunction.AsString("LINEAR"));

            outputHasBias = OutputHasBias.AsBoolean(true);

            inputs = Inputs.As2DArray<double>();

            targets = Targets.As2DArray<double>();

            errorTolerance = ErrorTolerance.AsDouble();

            epochLimit = EpochLimit.AsInt();
        }

        /// <inheritdoc />
        public override BasicNetwork Calculate()
        {
            //Network
            var network = new BasicNetwork();
            var inputNeuronCount = inputs.GetLength(1);
            var outputNeuronCount = targets.GetLength(1);

            //Input Layer
            var inputLayer = new BasicLayer(inputActivationFunction, inputHasBias, inputNeuronCount);
            network.AddLayer(inputLayer);

            //Hidden Layer
            if (hiddenLayerConfig.GetLength(1) != 2)
            {
                throw new ArgumentException("Net Configuration is a 2 column table of values of neuron count and activation function type, with each row representing a separate layer");
            }

            for (var row = 0; row < hiddenLayerConfig.GetLength(0); row++)
            {
                var activationFunc = Util.GetActivationFunction(hiddenLayerConfig[row, 1].ToString());
                var layer = new BasicLayer(activationFunc, true, Convert.ToInt32(hiddenLayerConfig[row, 0]));

                network.AddLayer(layer);
            }

            //Output layer
            var outputLayer = new BasicLayer(outputActivationFunction, outputHasBias, outputNeuronCount);
            network.AddLayer(outputLayer);

            //Training
            network.Structure.FinalizeStructure();
            network.Reset();
            var dataSet = new BasicMLDataSet(inputs.AsJagged(), targets.AsJagged());
            var trainlm = new LevenbergMarquardtTraining(network, dataSet);

            var epoch = 1;
            do
            {
                trainlm.Iteration();
                epoch++;
            } while (epoch < epochLimit && trainlm.Error > errorTolerance);

            return network;
        }

        /// <inheritdoc />
        public override object Render(BasicNetwork resultObject)
        {
            return ObjectStore.Add(name, resultObject);
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="functionType"></param>
        public TrainNetwork(Enums.FunctionType functionType = Enums.FunctionType.Light) : base(functionType)
        {
        }
    }
}
