using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;

namespace Majunga.Libraries.RazorComponents.Components.Forms
{
    public class EditorText : InputText
    {
        [Parameter] public string? Label { get; set; }
        [Parameter] public string? Id { get; set; }

        [Parameter] public bool OverrideClass { get; set; }

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(2, "class", "form-group");

            if (!string.IsNullOrEmpty(Label))
            {
                builder.OpenElement(3, "label");
                if (!string.IsNullOrEmpty(Id))
                    builder.AddAttribute(4, "for", Id);
                builder.AddAttribute(5, "class", "form-label");
                builder.AddContent(6, Label);
                builder.CloseElement();
            }

            var inputClass = OverrideClass ? CssClass : string.Join(' ', CssClass, "form-control mb-3");

            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", inputClass);
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
            builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string?>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
            builder.AddElementReferenceCapture(5, __inputReference => Element = __inputReference);
            builder.CloseElement();
            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string? value, out string? result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}
