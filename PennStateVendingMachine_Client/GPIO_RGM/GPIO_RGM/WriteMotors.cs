using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPIO_RGM
{
    class WriteMotors : GPIO_Write<int>
    {
        public WriteMotors(GPIOController.Pin[] relayPins, int[] columnIDs)
        {
            // Default to HIGH because Low toggles relay switch
            this.SetupPins(relayPins, columnIDs, GPIOController.State.High);
        }

        /// <summary>
        /// Starts the process of a vend motor turning.
        /// </summary>
        /// <param name="motorID">Column ID of the motor to turn</param>
        public void TurnMotor(int motorID)
        {
            // Turn motor on for 250 msec
            this.TurnMotorTime(motorID, 250);
            // Motor will continue to turn b/c of cam gear
            return;
        }

        /// <summary>
        /// Toggles a motor on for a specified amount of time.
        /// </summary>
        /// <param name="motorID">Column ID of the motor to turn</param>
        /// <param name="msec">Time, in msec, to leave on</param>
        public void TurnMotorTime(int motorID, int msec)
        {
            // Turn motor on for 250 msec
            int index = this.FindIndex(motorID);

            this.pins[index].SetLow();  // Set low -> turns on motor
            System.Threading.Thread.Sleep(msec); // Sleep for 250 msec
            this.pins[index].SetHigh(); // Set high again -> turn off motor 
            return;
        }

        // DEBUGGING FUNCTIONS
        public void SetHigh(int motorID)
        {
            int index = this.FindIndex(motorID);
            this.pins[index].SetHigh();
        }
        public void SetLow(int motorID)
        {
            int index = this.FindIndex(motorID);
            this.pins[index].SetLow();
        }
    }
}
