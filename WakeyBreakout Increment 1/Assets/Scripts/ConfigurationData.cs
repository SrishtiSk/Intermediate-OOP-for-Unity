using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // configuration data
    float paddleMoveUnitsPerSecond = 10;
    float ballImpulseForce = 200;
    float ballLifeTime = 10;
    float minSpawnSeconds = 5;
    float maxSpawnSeconds = 10;
    


    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    /// <summary>
    /// Gets the impulse force to apply to move the ball
    /// </summary>
    /// <value>impulse force</value>
    public float BallImpulseForce
    {
        get { return ballImpulseForce; }    
    }
    public float BallLifeTime
    {
        get { return ballLifeTime; }
    }
    public float MinSpawnSeconds
    {
        get { return minSpawnSeconds; }
    }
    public float MaxSpawnSeconds
    {
        get { return maxSpawnSeconds; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        StreamReader input = null;
        try
        {
            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, ConfigurationDataFileName));

            //read in names and values
            string names = input.ReadLine();
            string values = input.ReadLine();

            //set config data files
            SetConfigurationDataFields(values);
        }
       
        catch(Exception e)
        { 
        }
        finally
        {
            if (input != null)
                input.Close();
        }
    }
    void SetConfigurationDataFields(string csvValues)
    {
        string[] values = csvValues.Split(',');
        paddleMoveUnitsPerSecond = float.Parse(values[0]);
        ballImpulseForce = float.Parse(values[1]);
    }

    #endregion
}
