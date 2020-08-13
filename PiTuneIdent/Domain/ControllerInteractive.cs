﻿namespace PiTuneIdent.Domain
{
    /// <summary>
    /// Noninteractive Controller Algorithm. P = Kc (Controller Gain); I = Ti (Integral Time); D = Td (Derivative Time).   
    /// </summary>
    public class ControllerInteractive : ControllerModel, ITransferFunction
    {
        /// <summary>
        /// Creating PID Interactive Controller.
        /// </summary>
        /// <param name="p">Controller Gain</param>
        /// <param name="i">Integral Time</param>
        /// <param name="d">Derivative Time</param>
        public ControllerInteractive(double p = 1, double i = 0, double d = 0)
        {
            TypeAlg = TypeAlgorithm.Noninteractive;
            P = p;
            I = i;
            D = d;
        }
        
        /// Computation out signal after controller.
        public double TransferFunction(double input)
        {
            //TODO ControllerNoninteractive TransferFunction
            return input;
        }

        /// <summary>
        /// Converting Interactive to Noninteractive Controller Algorithm.
        /// Only for P or PI controller.
        /// </summary>
        /// <param name="ctr">Interactive Controller Algorithm </param>
        public void Convert(ControllerInteractive ctr)
        {
            if (ctr.D == 0)
            {
                P = ctr.P;
                I = ctr.I;
                D = 0;
            }
        }

        /// <summary>
        /// Converting Parallel to Noninteractive Controller Algorithm.
        /// Only for P or PI controller.
        /// </summary>
        /// <param name="ctr">Parallel Controller Algorithm </param>
        public void Convert(ControllerParallel ctr)
        {
            if(ctr.D == 0)
            {
                P = ctr.P;
                I = ctr.P / ctr.I;
                D = 0;
            }
        }

        /// <summary>
        /// Converting CentumPID to Noninteractive Controller Algorithm.
        /// Only for P or PI controller.
        /// </summary>
        /// <param name="ctr">CentumPID Controller Algorithm </param>
        public void Convert(ControllerCentumPID ctr)
        {
            if (ctr.D == 0)
            {
                P = 100 / ctr.P;
                I = ctr.I;
                D = 0;
            }
        }
    }
}
