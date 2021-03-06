﻿using Encog.Engine.Network.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;

namespace Metasense.Infrastructure
{
    public static class Util
    {

        /// <summary>
        /// The Excel range from which the current function is being called. This will only return a value when invoked from within the code path of an invoked function
        /// </summary>
        /// <returns></returns>
        public static ExcelReference GetCallingRange()
        {
            return XlCall.Excel(XlCall.xlfCaller) as ExcelReference;
        }
        
        /// <summary>
        /// Instantiate an activation function based on the string reference
        /// </summary>
        /// <param name="activationFunction"></param>
        /// <returns></returns>
        public static IActivationFunction GetActivationFunction(string activationFunction)
        {
            switch (activationFunction.ToUpper())
            {
                case "BIPOLAR":
                    return new ActivationBiPolar();
                case "BIPOLARSS":
                    return new ActivationBipolarSteepenedSigmoid();
                case "CLIPPEDLINEAR":
                    return new ActivationClippedLinear();
                case "ELLIOT":
                    return new ActivationElliott();
                case "ELLIOTSYMMETRIC":
                    return new ActivationElliottSymmetric();
                case "GAUSSIAN":
                    return new ActivationGaussian();
                case "LINEAR":
                    return new ActivationLinear();
                case "LOG":
                    return new ActivationLOG();
                case "SIGMOID":
                    return new ActivationSigmoid();
                case "SIN":
                    return new ActivationSIN();
                case "SOFTMAX":
                    return new ActivationSoftMax();
                case "STEEPENDSIGMOID":
                    return new ActivationBipolarSteepenedSigmoid();
                case "TANH":
                    return new ActivationTANH();
                default:
                    throw new ArgumentException("String '" + activationFunction + "' did not match any available activation functions");
            }
        }
    }
}
