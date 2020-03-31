using AIR.Tilde;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class TildeLogTests {
    private GameObject _testGoRoot;
    private TildeLog _tildeLog;
    
    const string LOG_MSG = "Log Message";
    const string LOG_WARNING_MSG = "Warning Message";
    const string LOG_ERROR_MSG = "Error Message";
    
    [SetUp]
    public void SetUp() {
        _testGoRoot = new GameObject("TestGo");
        _testGoRoot.AddComponent<Tilde>().Start();
        _tildeLog = _testGoRoot.GetComponent<TildeLog>();
    }

    [TearDown]
    public void TearDown() {
        Object.Destroy(_testGoRoot);
    }

    [Test]
    public void DebugLog_TildeInScene_AddsStringToLog() {
        
        // Act
        Debug.Log(LOG_MSG);

        // Assert
        StringAssert.Contains(LOG_MSG,_tildeLog.LogsToString());
    }
    
    [Test]
    public void DebugLogWarning_TildeInScene_AddsStringToLog() {

        // Act
        Debug.LogWarning(LOG_WARNING_MSG);

        // Assert
        StringAssert.Contains(LOG_WARNING_MSG,_tildeLog.LogsToString());
    }
    
    [Test]
    public void DebugLogError_TildeInScene_AddsStringToLog() {
        // Arrange
        LogAssert.Expect(LogType.Error, LOG_ERROR_MSG);

        // Act
        Debug.LogError(LOG_ERROR_MSG);

        // Assert
        StringAssert.Contains(LOG_ERROR_MSG,_tildeLog.LogsToString());
    }

    [Test]
    public void TildeLog_TildeInScene_AddsStringToLog() {
        
        // Act
        Tilde.Log(LOG_MSG);

        // Assert
        StringAssert.Contains(LOG_MSG,_tildeLog.LogsToString());
    }
    
    [Test]
    public void TildeLogWarning_TildeInScene_AddsStringToLog() {

        // Act
        Tilde.LogWarning(LOG_WARNING_MSG);

        // Assert
        StringAssert.Contains(LOG_WARNING_MSG,_tildeLog.LogsToString());
    }
    
    [Test]
    public void TildeLogError_TildeInScene_AddsStringToLog() {

        // Act
        Tilde.LogError(LOG_ERROR_MSG);

        // Assert
        StringAssert.Contains(LOG_ERROR_MSG,_tildeLog.LogsToString());
    }
    
}
