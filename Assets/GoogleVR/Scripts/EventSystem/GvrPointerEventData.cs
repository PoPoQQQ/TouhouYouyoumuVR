//-----------------------------------------------------------------------
// <copyright file="GvrPointerEventData.cs" company="Google Inc.">
// Copyright 2018 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This script extends the Unity PointerEventData struct with GoogleVR-specific data.
/// </summary>
public class GvrPointerEventData : PointerEventData
{
    /// <summary>The mask of buttons that are currently down.</summary>
    public GvrControllerButton gvrButtonsDown;

    /// <summary>
    /// Initializes a new instance of the <see cref="GvrPointerEventData" /> class.
    /// </summary>
    /// <param name="eventSystem">The event system associated with this event.</param>
    public GvrPointerEventData(EventSystem eventSystem) : base(eventSystem)
    {
    }
}
