
using System;
using AIR.Tilde;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class TildeTests {

    [Test]
    public void Instantiation_TildeInstance_HasRequiredComponents(
        [Values(typeof(TildeCmd), typeof(TildeLog))] Type collaborator
    ) {
        // Arrange
        var tildeGo = new GameObject("TestGO", typeof(Tilde));
        
        // Act
        var collaboratorComponent = tildeGo.GetComponent(collaborator);

        // Assert
        Assert.IsNotNull(collaboratorComponent);
    }

}
