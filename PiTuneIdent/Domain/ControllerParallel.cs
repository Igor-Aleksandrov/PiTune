﻿namespace PiTuneIdent.Domain
{
    /// <summary>
    /// Parallel Controller Algorithm. P = Kp (Proportional Gain); I = Ki (Integral Gain); D = Kd (Derivative Time).   
    /// </summary>
    class ControllerParallel : ControllerModel, ITransferFunction
    {
        public ControllerParallel()
        {
            TypeAlg = TypeAlgorithm.Noninteractive;
            P = 1;
            I = 0;
            D = 0;
        }

        /// <summary>
        /// Creating PID Parallel Controller.
        /// </summary>
        /// <param name="p">Proportional Gain</param>
        /// <param name="i">Integral Gain</param>
        /// <param name="d">Derivative Time</param>
        public ControllerParallel(double p = 1, double i = 0, double d = 0)
        {
            TypeAlg = TypeAlgorithm.Noninteractive;
            P = p;
            I = i;
            D = d;
        }

        /// Computation out signal after controller.
        public double TransferFunction(double input)
        {
            //TODO ControllerParallel TransferFunction
            return input;
        }

        /// <summary>
        /// Converting Interactive to Parallel Controller Algorithm.
        /// </summary>
        /// <param name="ctr">Interactive Controller Algorithm </param>
        public void InterToParallelr(ControllerInteractive ctr)
        {
            P = ctr.P * (1 + ctr.D / ctr.I);
            I = ctr.P / ctr.I;
            D = ctr.P * ctr.D;
        }

        /// <summary>
        /// Converting Noninteractive to Parallel Controller Algorithm.
        /// </summary>
        /// <param name="ctr">Noninteractive Controller Algorithm </param>
        public void NoninterToParallel(ControllerNoninteractive ctr)
        {
            P = ctr.P;
            I = ctr.P / ctr.I;
            D = ctr.D * ctr.P;
        }

        /// <summary>
        /// Converting CentumPID to Parallel Controller Algorithm.
        /// </summary>
        /// <param name="ctr">CentumPID Controller Algorithm </param>
        public void CentumPIDToParallel(ControllerCentumPID ctr)
        {
            P = 100 / ctr.P;
            I = 100 / (ctr.P * ctr.I);
            D = 100 * ctr.D / ctr.P;
        }
    }
}
