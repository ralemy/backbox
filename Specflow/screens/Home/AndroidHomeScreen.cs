﻿using System;
using Xamarin.UITest.Queries;

namespace Specflow.Screens
{
    public class AndroidHomeScreen : IHomeScreen
    {
        public Func<AppQuery, AppQuery> addButton { get; } = new Func<AppQuery, AppQuery>(c => c.Marked("Add Task"));
    }
}
