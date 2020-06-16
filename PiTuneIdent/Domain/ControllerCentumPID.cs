namespace PiTuneIdent.Domain
{
    /// <summary>
    /// CentumPID Controller Algorithm. P = PB (Proportional band); I = Ti (Integral Time); D = Td (Derivative Time).   
    /// </summary>
    class ControllerCentumPID : ControllerModel, ITransferFunction
    {
        public ControllerCentumPID()
        {
            TypeAlg = TypeAlgorithm.Noninteractive;
            P = 1;
            I = 1;
            D = 0;
        }
        /// <summary>
        /// Creating CentumPID Controller.
        /// </summary>
        /// <param name="p">Proportional band</param>
        /// <param name="i">Integral Time</param>
        /// <param name="d">Derivative Time</param>
        public ControllerCentumPID(double p = 100, double i = 0, double d = 0)
        {
            TypeAlg = TypeAlgorithm.Noninteractive;
            P = p;
            I = i;
            D = d;
        }

        /// Computation out signal after controller.
        public double TransferFunction(double input)
        {
            //TODO ControllerCentumPID TransferFunction
            return input;
        }

        /// <summary>
        /// Converting Interactive to CentumPID Controller Algorithm.
        /// </summary>
        /// <param name="ctr">Interactive Controller Algorithm </param>
        public void Convert(ControllerInteractive ctr)
        {
            P = 100 * ctr.I / (ctr.P * (ctr.I + ctr.D));
            I = ctr.I + ctr.D;
            D = ctr.I * ctr.D / (ctr.I + ctr.D);
        }

        /// <summary>
        /// Converting Parallel to CentumPID Controller Algorithm.
        /// </summary>
        /// <param name="ctr">Parallel Controller Algorithm </param>
        public void Convert(ControllerParallel ctr)
        {
            P = 100 / ctr.P;
            I = ctr.P / ctr.I;
            D = ctr.D / ctr.P;
        }

        /// <summary>
        /// Converting Noninteractive to CentumPID Controller Algorithm.
        /// </summary>
        /// <param name="ctr">Noninteractive Controller Algorithm </param>
        public void Convert(ControllerNoninteractive ctr)
        {
            P = 100 / ctr.P;
            I = ctr.I;
            D = ctr.D;
        }
    }
}
