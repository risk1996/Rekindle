using System;

public interface IBeater {

  public void Beat();

  public UInt32 BeatRemainder { get; }
}
