using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiTuneIdent.Domain
{
    /// <summary>
    /// Contains model's parameters. Ones describe the control object through the transfer function.
    /// The parameters are calculated experimentally by indetification or by analytical methods.
    /// The parameters of the control object are necessary for calculating the PID controler's tunning parameters of the control loop.
    /// </summary>
    abstract class ObjectModel
    {
        private double dt;
        private double tau1;
        private double tau2;
        private double beta;

        //if (value >= 0) dt = value;

        /// <summary>
        /// Process gain.
        /// Gp = change in PV [in %] / change in CO [in %].
        /// </summary>
        public double Gp { get; set; }

        /// <summary>
        /// Dead time.
        /// Difference between the step-change in CO and the beginning of change PV response curve.
        /// </summary>
        public double Dt {
            get { return dt; }
            set
            {
                dt = (value >= 0) ? value : 1; //TODO contract programming
            }
        }

        /// <summary>
        /// Time constant.
        /// Difference between intersection at the end of dead time, and the PV reaching 63% of its total change.
        /// </summary>
        public double Tau1 {
            get { return tau1; }
            set
            {
                tau1 = (value >= 0) ? value : 0; //TODO contract programming
            }
        }

        /// <summary>
        /// Time constant of second order.
        /// </summary>
        public double Tau2 {
            get { return tau2; }
            set
            {
                tau2 = (value >= 0) ? value : 0; //TODO contract programming
            }
        }

        /// <summary>
        /// Time constant of second order..
        /// </summary>
        public double Beta {
            get { return beta; }
            set
            {
                beta = (value >= 0) ? value : 0; //TODO contract programming
            }
        }
    }

    class ObjectModels : ObjectModel
    {
        
        /// <summary>
        /// Blank model. Gp=1, Td=0, Tau1=0, Tau2=0, Beta=0.
        /// </summary>
        public ObjectModels()
        {
            Gp = 1.0;
            Dt = 0.0;
            Tau1 = 0.0;
            Tau2 = 0.0;
            Beta = 0.0;
        }

        /// <summary>
        /// 1st order model. Tau2=0, Beta=0.
        /// </summary>
        public ObjectModels(double gp, double td, double tau1)
        {
            Gp = gp;
            Dt = td;
            Tau1 = tau1;
            Tau2 = 0.0;
            Beta = 0.0;
        }
    }
}
