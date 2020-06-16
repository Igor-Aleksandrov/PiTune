using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiTuneIdent.Domain
{
    /// <summary>
    /// The Proportional, Integral and Derivative modes are arranged into different controller algorithms or controller structures:
    /// 0 - Noninteractive Algorithm;
    /// 1 - Interactive Algorithm;
    /// 2 - Parallel Algorithm;
    /// 3 - CentumVP basic type PID control.
    /// </summary>
    public enum TypeAlgorithm
    {
        Noninteractive = 0,
        Interactive,
        Parallel,
        CentumPID
    }
    
    /// <summary>
    /// Contains a controller's tunning parameters.
    /// </summary>
    abstract class ControllerModel
    {
        private double i;
        /// <summary>
        /// The Proportional, Integral and Derivative modes are arranged into different controller algorithms or controller structures:
        /// 0 - Noninteractive Algorithm;
        /// 1 - Interactive Algorithm;
        /// 2 - Parallel Algorithm;
        /// 3 - CentumVP basic type PID control.
        /// </summary>
        internal TypeAlgorithm TypeAlg { get; set; }

        /// <summary>
        /// Proportional parameter (Kc, Kc, Kp or PB).
        /// </summary>
        public double P { get; set; }

        /// <summary>
        /// Integral parameter (Ti, Ti, Ki or Ti).
        /// </summary>
        public double I {
            get { return i; }
            set
            {
                i = (value == 0) ? 1 : value; //TODO contract programming
            }
        }

        /// <summary>
        /// Derivative parameter (Td, Td, Kd or Td).
        /// </summary>
        public double D { get; set; }
    }
}
