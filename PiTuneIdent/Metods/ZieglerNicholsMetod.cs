using PiTuneIdent.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiTuneIdent.Metods
{
    /// <summary>
    /// Calculating settings for PID Controller Gain (Kc), Integral Time (Ti), and Derivative Time (Td) using the Ziegler-Nichols tuning rules.
    /// The Ziegler-Nichols tuning rules were designed for controllers with the interactive controller algorithm.
    /// The Ziegler-Nichols tuning rules work well on processes of which the time constant is at least two times as long as the dead time.
    /// </summary>
    class ZieglerNicholsMetod
    {
        /// <summary>
        /// Calculating settings for P Controller Gain (Kc= tau / (gp * td)) using the Ziegler-Nichols tuning rules.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static ControllerInteractive TuningP(ObjectModel oM)
        { 
            // Calculating Controller Gain (Kc)
            double P = oM.Tau1 / (oM.Gp * oM.Dt);

            return new ControllerInteractive(P);
        }

        /// <summary>
        /// Calculating settings for PI Controller Gain (Kc = 0.9 * tau / (gp * td)) and Integral Time (Ti = 3.33 * td) using the Ziegler-Nichols tuning rules.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static ControllerInteractive TuningPI(ObjectModel oM)
        {
            // Calculating Controller Gain (Kc)
            double P = 0.9 * oM.Tau1 / (oM.Gp * oM.Dt);
            // Calculating Integral Time (Ti)
            double I = 3.33 * oM.Dt;

            return new ControllerInteractive(P, I);
        }

        /// <summary>
        /// Calculating settings for PID Controller Gain (Kc), Integral Time (Ti), and Derivative Time (Td) using the Ziegler-Nichols tuning rules.
        /// For PID control: Kc = 1.2 * tau / (gp * td); Ti = 2 * td; Td = 0.5 * td.
        /// </summary>
        /// <param name="oM">Contains model's parameters. Ones describe the control object through the transfer function.</param>
        /// <param name="cPID">Contains a controller's tunning parameters.</param>
        public static ControllerInteractive TuningPID(ObjectModel oM)
        {
            // Calculating Controller Gain (Kc)
            double P = 1.2 * oM.Tau1 / (oM.Gp * oM.Dt);
            // Calculating Integral Time (Ti)
            double I = 2 * oM.Dt;
            // Calculating Derivative Time (Td)
            double D = 0.5 * oM.Dt;

            return new ControllerInteractive(P, I, D);
        }
    }
}
