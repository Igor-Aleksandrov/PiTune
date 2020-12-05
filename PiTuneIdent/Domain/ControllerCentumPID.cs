namespace PiTuneIdent.Domain
{
    /// <summary>
    /// CentumPID Controller Algorithm. P = PB (Proportional band); I = Ti (Integral Time); D = Td (Derivative Time).   
    /// </summary>
    public class ControllerCentumPID : ControllerModel, ITransferFunction
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
        /// Calculation of an output trend of the PID controller.
        /// </summary>
        /// <param name="x"> Input Deviation(Ei=PVi-SVi)</param>
        /// <param name="y0"></param>
        /// <param name="x0"></param>
        /// <returns>Process variable after the element</returns>
        public double[,] CalcTrendD(double Gp = 2, double Tau1 = 60, double sv0 = 60, double pv0 = 40, double mv0 = 20)
        {
            int delta = 1; // Time different between x[i] and x[i-1]
            int len = 300 * delta;
            double[,] y = new double[4, len];

            P = 100;
            // stable state
            y[0, 0] = pv0;                  // PV
            y[1, 0] = pv0;                  // SV
            y[3, 0] = y[0, 0] - y[1, 0];    // E = PV - SV
            y[2, 0] = mv0;                  // MV

            y[0, 1] = y[0, 0];              // PV
            y[1, 1] = sv0;                  // SV
            y[3, 1] = y[0, 1] - y[1, 1];    // E = PV - SV
            y[2, 1] = y[2, 0] - (y[3, 1] - y[3, 0] + y[3, 1] * delta / I + (y[3, 1] - 2 * y[3, 0]) * D / delta) * 100 / P; // MV

            // next PV, MV calculated via a linear difference equation
            for (int i = 2; i < len; i++)
            {
                y[0, i] = y[2, i - 1] * delta * Gp / (Tau1 + delta) + y[0, i - 1] * Tau1 / (Tau1 + delta); //PV
                y[1, i] = y[1, i - 1];                  // SV
                y[3, i] = y[0, i] - y[1, i];    // E = PV - SV
                y[2, i] = y[2, i - 1] - (y[3, i] - y[3, i - 1] + y[3, i] * delta / I + (y[3, i] - 2 * y[3, i - 1] + y[3, i - 2]) * D / delta) * 100 / P; //MV
            }

            return y;
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
