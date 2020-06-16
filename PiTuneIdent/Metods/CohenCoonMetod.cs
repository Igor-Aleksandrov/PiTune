using PiTuneIdent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiTuneIdent.Metods
{
    /// <summary>
    /// Calculating settings for PID Controller Gain (Kc), Integral Time (Ti), and Derivative Time (Td) using the Cohen-Coon tuning rules.
    /// The Cohen-Coon tuning rules were designed for controllers with the Noninteractive controller algorithm.
    /// The Cohen-Coon tuning rules work well on processes where the dead time is less than two times the length of the time constant (and you can stretch this even further if required).
    /// The Cohen-Coon tuning rules are suitable for use on self-regulating processes if the control objective is having a fast response.
    /// </summary>
    class CohenCoonMetod
    {
        /// <summary>
        /// Calculating settings for P Controller Gain (Kc= (0.34 + tau / td) * 1.03 / gp) using the Cohen-Coon tuning rules.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void TuningP(ObjectModel oM, ControllerModel cPID)
        {
            // Calculating Controller Gain (Kc)
            cPID.P = (0.34 + oM.Tau1 / oM.Dt) * 1.03 / oM.Gp;
        }

        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc= (0.092 + tau / td) * 0.9 / gp) and Integral Time (Ti = 3.33 * td * (tau + 0.092 * td) / (tau + 2.22 * td)) using the Cohen-Coon tuning rules.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void TuningPI(ObjectModel oM, ControllerModel cPID)
        {
            // Calculating Controller Gain (Kc)
            cPID.P = (0.092 + oM.Tau1 / oM.Dt) * 0.9 / oM.Gp;
            // Calculating Integral Time (Ti)
            cPID.I = 3.33 * oM.Dt * (oM.Tau1 + 0.092 * oM.Dt) / (oM.Tau1 + 2.22 * oM.Dt);
        }

        /// <summary>
        /// Calculating settings for PD Controller Gain (Kc= (0.129 + tau / td) * 1.24 / gp) and Derivative Time (Td = 0.27 * td * (tau - 0.342 * td) / (tau + 0.129 * td)) using the Cohen-Coon tuning rules.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void TuningPD(ObjectModel oM, ControllerModel cPID)
        {
            // Calculating Controller Gain (Kc)
            cPID.P = (0.129 + oM.Tau1 / oM.Dt) * 1.24 / oM.Gp;
            // Calculating Integral Time (Td)
            cPID.D = 0.27 * oM.Dt * (oM.Tau1 - 0.324 * oM.Dt) / (oM.Tau1 + 0.129 * oM.Dt);
        }

        /// <summary>
        /// Calculating settings for PID Controller Gain (Kc), Integral Time (Ti), and Derivative Time (Td) using the Cohen-Coon tuning rules.
        /// For PID control: Kc = (0.185 + tau / td) * 1.35 / gp; Ti = 2.5 * td * (tau + 0.185 * td) / (tau + 0.611 * td); Td = 0.37 * td * tau / (tau + 0.185 * td).
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static void TuningPID(ObjectModel oM, ControllerModel cPID)
        {
            if (cPID is ControllerNoninteractive)
            {
                // Calculating Controller Gain (Kc)
                cPID.P = (0.185 + oM.Tau1 / oM.Dt) * 1.35 / oM.Gp;
                // Calculating Integral Time (Ti)
                cPID.I = 2.5 * oM.Dt * (oM.Tau1 + 0.185 * oM.Dt) / (oM.Tau1 + 0.611 * oM.Dt);
                // Calculating Derivative Time (Td)
                cPID.D = 0.37 * oM.Dt * oM.Tau1 / (oM.Tau1 + 0.185 * oM.Dt);
            }
        }
    }
}
