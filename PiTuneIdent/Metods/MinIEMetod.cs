using PiTuneIdent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiTuneIdent.Metods
{
    /// <summary>
    /// Calculating settings for PID Controller Gain (Kc), Integral Time (Ti), and Derivative Time (Td) using the Minimum error-integral tuning rules.
    /// The Minimum error-integral tuning rules were designed for controllers with the noninteractive controller algorithm.
    /// The tuning rules were developed for optimizing a control loop’s disturbance response. 
    /// The Minimum error-integral tuning rules were designed only for processes with time-constants equal to or longer than dead times (tau >= td).
    /// </summary>
    class MinIEMetod
    {
        private static double[,] constIE = new double[6, 6]
                                        {
                                            {1.305, -0.959, 0.492, 0.739, 0, 0},            // ISE PI
                                            {1.495, -0.945, 1.101, 0.771, 0.560, 1.006},    // ISE PID
                                            {0.984, -0.986, 0.608, 0.707, 0, 0},            // IAE PI
                                            {1.435, -0.921, 0.878, 0.749, 0.482, 1.137},    // IAE PID
                                            {0.859, -0.977, 0.674, 0.680, 0, 0},            // ITAE PI
                                            {1.357, -0.947, 0.842, 0.738, 0.381, 0.995}     // ITAE PID
                                        };

        /// <summary>
        /// Calculating settings for PI/PID using different constants for each IE methods.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        private static void TuningIE(ref ObjectModel oM, ref ControllerModel cPID, int i)
        {
            // Calculating Controller Gain (Kc)
            cPID.P = constIE[i,0] * Math.Pow(oM.Dt / oM.Tau1, constIE[i,1]) / oM.Gp;
            // Calculating Integral Time (Ti)
            cPID.I = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, constIE[i,3]) / constIE[i,2];
            // Calculating Derivative Time (Td)
            cPID.D = oM.Tau1 * constIE[i,4] * Math.Pow(oM.Dt / oM.Tau1, constIE[i,5]);
        }

        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc) and Integral Time (Ti) using the Integral of the error squared tuning rules.
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 1.305, -0.959, 0.492, 0.739, 0, 0.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void ISEtuningPI(ObjectModel oM, ControllerModel cPID)
        {
            TuningIE(ref oM, ref cPID, 0);
        }

        /// <summary>
        /// Calculating settings for PID Controller Gain (Kc) and Integral Time (Ti) using the Integral of the error squared tuning rules.
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 1.495, -0.945, 1.101, 0.771, 0.560, 1.006.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void ISEtuningPID(ObjectModel oM, ControllerModel cPID)
        {
            TuningIE(ref oM, ref cPID, 1);
        }

        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc) and Integral Time (Ti) using the Integral of the absolute error tuning rules.
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 0.984, -0.986, 0.608, 0.707, 0, 0.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void IAEtuningPI(ObjectModel oM, ControllerModel cPID)
        {
            TuningIE(ref oM, ref cPID, 2);
        }

        /// <summary>
        /// Calculating settings for PID Controller Gain (Kc) and Integral Time (Ti) using the Integral of the absolute error tuning rules.
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 1.435, -0.921, 0.878, 0.749, 0.482, 1.137.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void IAEtuningPID(ObjectModel oM, ControllerModel cPID)
        {
            TuningIE(ref oM, ref cPID, 3);
        }

        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc) and Integral Time (Ti) using the Integral of the absolute error multiplied by time tuning rules.
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 0.859, -0.977, 0.674, 0.680, 0, 0.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void ITAEtuningPI(ObjectModel oM, ControllerModel cPID)
        {
            TuningIE(ref oM, ref cPID, 4);
        }

        /// <summary>
        /// Calculating settings for PID Controller Gain (Kc) and Integral Time (Ti) using the Integral of the absolute error multiplied by time tuning rules.
        /// Kc = A * Math.Pow(oM.Dt / oM.Tau1, B) / oM.Gp; Ti = oM.Tau1 * Math.Pow(oM.Dt / oM.Tau1, D) / C; Td = oM.Tau1 * E * Math.Pow(oM.Dt / oM.Tau1, F).
        /// A, B, C, D, E, F = 1.357, -0.947, 0.842, 0.738, 0.381, 0.995.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void ITAEtuningPID(ObjectModel oM, ControllerModel cPID)
        {
            TuningIE(ref oM, ref cPID, 5);
        }
    }
}
