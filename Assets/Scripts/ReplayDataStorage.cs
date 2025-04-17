using System;
using System.Collections.Generic;

public static class ReplayDataStorage
{
    public static List<ActionReplayRecord> SavedReplayRecords = new List<ActionReplayRecord>();

    // Timestamp
    public static DateTime ReplayStartTime;
}

