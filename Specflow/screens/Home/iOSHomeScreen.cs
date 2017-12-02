﻿using System;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{

    public class iOSHomeScreen : IHomeScreen
    {
        public Func<AppQuery, AppQuery> addButton { get; } = new Func<AppQuery, AppQuery>(c => c.Marked("Add"));
    }
}

