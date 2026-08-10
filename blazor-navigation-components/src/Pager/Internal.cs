using Microsoft.AspNetCore.Components.Web;



namespace Syncfusion.Blazor.Navigations.Internal
{
    /// <summary>
    /// Handles the pager locale key.
    /// </summary>
    
    internal static class PagerKeyUtilExtension
    {
        private const string tabKey = "Tab";
        private const string shiftTabKey = "ShiftTab";
        private const string enterKey = "Enter";
        private const string spaceKey = "Space";
        private const string arrowLeftKey = "ArrowLeft";
        private const string arrowRightKey = "ArrowRight";
        private const string homeKey = "Home";
        private const string endKey = "End";
        private const string pageUpKey = "PageUp";
        private const string pageDownKey = "PageDown";
        private const string altPageUpKey = "AltPageUp";
        private const string altPageDownKey = "AltPageDown";
        private const string ctrlAltPageUpKey = "CtrlAltPageUp";
        private const string ctrlAltPageDownKey = "CtrlAltPageDown";

        private static bool IsEnter(this KeyboardEventArgs e) => e.Key == enterKey;

        private static bool IsSpace(this KeyboardEventArgs e) => e.Code == spaceKey;

        private static bool IsTab(this KeyboardEventArgs e) => e.Key == tabKey;

        private static bool IsShiftTab(this KeyboardEventArgs e) => e.IsTab() && e.ShiftKey;

        private static bool IsLeftArrow(this KeyboardEventArgs e) => e.Key == arrowLeftKey;

        private static bool IsRightArrow(this KeyboardEventArgs e) => e.Key == arrowRightKey;

        private static bool IsArrowKey(this KeyboardEventArgs e) => e.IsLeftArrow() || e.IsRightArrow();

        private static bool IsHome(this KeyboardEventArgs e) => e.Key == homeKey;

        private static bool IsEnd(this KeyboardEventArgs e) => e.Key == endKey;


        private static bool IsPageUp(this KeyboardEventArgs e) => e.Key == pageUpKey;

        private static bool IsPageDown(this KeyboardEventArgs e) => e.Key == pageDownKey;

        // ctrlAltPageUp: 'ctrl+alt+pageup',
        private static bool IsCtrlAltPageUp(this KeyboardEventArgs e) => e.IsPageUp() && e.CtrlKey && e.AltKey;

        // ctrlAltPageDown: 'ctrl+alt+pagedown',
        private static bool IsCtrlAltPageDown(this KeyboardEventArgs e) => e.IsPageDown() && e.CtrlKey && e.AltKey;

        // altPageUp: 'alt+pageup',
        private static bool IsAltPageUp(this KeyboardEventArgs e) => e.IsPageUp() && e.AltKey;

        // altPageDown: 'alt+pagedown',
        private static bool IsAltPageDown(this KeyboardEventArgs e) => e.IsPageDown() && e.AltKey;

        public static string GetKeyCombination(this KeyboardEventArgs e)
        {
            string? action = null;

            if (e.IsEnter())
            {
                action = enterKey;
            }
            else if (e.IsTab())
            {
                action = tabKey;
                if (e.IsShiftTab())
                {
                    action = shiftTabKey;
                }
            }
            else if (e.IsArrowKey())
            {
                action = ArrowKeyHandler(e, action!);
            }
            else if (e.IsHome())
            {
                action = homeKey;
            }
            else if (e.IsEnd())
            {
                action = endKey;
            }
            else if (e.IsSpace())
            {
                action = spaceKey;
            }            
            else if (e.IsPageDown())
            {
                action = pageDownKey;

                if (e.IsAltPageDown())
                {
                    action = altPageDownKey;
                }

                if (e.IsCtrlAltPageDown())
                {
                    action = ctrlAltPageDownKey;
                }
            }
            else if (e.IsPageUp())
            {
                action = pageUpKey;

                if (e.IsAltPageUp())
                {
                    action = altPageUpKey;
                }

                if (e.IsCtrlAltPageUp())
                {
                    action = ctrlAltPageUpKey;
                }
            }

            return action ?? string.Empty;
        }

        private static string ArrowKeyHandler(KeyboardEventArgs e, string action)
        {

            if (e.IsLeftArrow())
            {
                action = arrowLeftKey;
            }

            if (e.IsRightArrow())
            {
                action = arrowRightKey;
            }
            return action;
        }
    }
}
