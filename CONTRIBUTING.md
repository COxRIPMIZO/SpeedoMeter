# Contributing to SpeedoMeter

Thank you for your interest in contributing to SpeedoMeter! We welcome contributions from the community. This document provides guidelines and instructions for contributing to the project.

## 📋 Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Environment](#development-environment)
- [Coding Standards](#coding-standards)
- [Git Workflow](#git-workflow)
- [Submitting Changes](#submitting-changes)
- [Testing](#testing)
- [Documentation](#documentation)
- [Reporting Issues](#reporting-issues)
- [Feature Requests](#feature-requests)

---

## 🤝 Code of Conduct

By participating in this project, you agree to:
- Be respectful and inclusive to all contributors
- Provide constructive feedback
- Focus on what's best for the community
- Report unacceptable behavior to the project maintainers

---

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2022 (Community, Professional, or Enterprise)
- .NET Framework 4.8 SDK
- Git for version control

### Fork & Clone

1. **Fork the Repository**
   - Visit [SpeedoMeter on GitHub](https://github.com/COxRIPMIZO/SpeedoMeter)
   - Click "Fork" in the top-right corner
   - Clone your fork locally:
   ```bash
   git clone https://github.com/[your-username]/SpeedoMeter.git
   cd SpeedoMeter
   ```

2. **Add Upstream Remote**
   ```bash
   git remote add upstream https://github.com/COxRIPMIZO/SpeedoMeter.git
   git fetch upstream
   ```

3. **Create Working Branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

---

## 💻 Development Environment

### Opening the Project

1. Open `SpeedoMeter.sln` in Visual Studio 2022
2. Restore NuGet packages (if any)
3. Build the solution (__Ctrl+Shift+B__)
4. Run tests to verify your environment

### Project Structure

```
SpeedoMeter/
├── SpeedoMeter/
│   ├── SpeedometerControl.cs          # Main control implementation
│   ├── SpeedometerControl.Designer.cs # Designer-generated code
│   ├── Form1.cs                       # Demo form
│   ├── Form1.Designer.cs              # Designer-generated code
│   ├── Form1.resx                     # Resources
│   └── Program.cs                     # Entry point
├── .editorconfig                      # Code style rules
├── README.md                          # Project documentation
├── CONTRIBUTING.md                    # This file
└── SpeedoMeter.sln                    # Solution file
```

### Build Configuration

- **Debug**: Full debugging information, no optimizations
- **Release**: Optimized build for distribution

---

## 📝 Coding Standards

### General Principles

- Follow the project's existing code style (enforced by `.editorconfig`)
- Write clear, self-documenting code
- Include XML documentation for public members
- Use meaningful variable and method names

### C# Naming Conventions

**Types (Classes, Structs, Enums)**:
```csharp
public class SpeedometerControl { }
public struct PointF { }
public enum NeedleStyle { }
```

**Interfaces**:
```csharp
public interface IGaugeProvider { }
```

**Properties & Methods**:
```csharp
public float Value { get; set; }
public void DrawNeedle(Graphics g, Rectangle rect) { }
```

**Private Fields**:
```csharp
private float _value = 0f;
private Color _needleColor1 = Color.Red;
```

**Local Variables**:
```csharp
float radius = rect.Width * 0.5f;
var centerPoint = new PointF(center.X, center.Y);
```

### Code Style Rules

**Indentation**:
- Use 4 spaces (no tabs)
- Enforced by `.editorconfig`

**Braces**:
```csharp
if (condition)
{
    DoSomething();
}
else
{
    DoSomethingElse();
}
```

**Spacing**:
```csharp
// Around binary operators
int result = x + y;

// After comma
var point = new PointF(1f, 2f);

// No space after cast
float value = (float)intValue;

// After keywords
for (int i = 0; i < count; i++)
{
}
```

**Using Statements**:
```csharp
// Group and order
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpeedoMeter
{
    public class Example { }
}
```

### Documentation Standards

**XML Documentation**:
```csharp
/// <summary>
/// Draws the needle on the gauge at the current value.
/// </summary>
/// <param name="g">The Graphics object to draw on</param>
/// <param name="rect">The bounding rectangle of the gauge</param>
private void DrawNeedle(Graphics g, Rectangle rect)
{
    // Implementation
}
```

**Inline Comments**:
```csharp
// Use for complex logic only
float normalizedValue = Clamp(value, MinValue, MaxValue);

// Map value to angle (avoid obvious comments)
float angle = MapValueToAngle(normalizedValue);
```

### Property Documentation

```csharp
/// <summary>
/// Gets or sets the current gauge reading, clamped to [MinValue, MaxValue].
/// </summary>
[Category("1. Gauge Core")]
[Description("Current gauge value")]
public float Value
{
    get => _value;
    set
    {
        _value = Clamp(value, MinValue, MaxValue);
        Invalidate();
    }
}
```

---

## 🔄 Git Workflow

### Commit Messages

Use clear, descriptive commit messages following this format:

```
[Category] Brief description (50 chars or less)

Optional detailed explanation if needed.
- Use bullet points for multiple changes
- Reference issues when relevant: Fixes #123
```

**Categories**:
- `[Feature]` - New functionality
- `[Fix]` - Bug fix
- `[Refactor]` - Code restructuring without behavior change
- `[Docs]` - Documentation only
- `[Style]` - Formatting, naming, etc.
- `[Performance]` - Performance optimization
- `[Test]` - Test additions or modifications

**Examples**:
```bash
git commit -m "[Feature] Add new needle style: VintageArtDeco

- Implements stepped multi-segment needle design
- Adds OrnateDiamondSizeMultiplier property
- Includes rendering method DrawNeedle_VintageArtDeco
- Fixes #42"

git commit -m "[Fix] Correct zone arc calculation for values near boundaries"

git commit -m "[Docs] Update README with API reference"
```

### Keeping Your Branch Updated

```bash
# Fetch latest upstream changes
git fetch upstream

# Rebase your branch on main
git rebase upstream/master

# If conflicts occur, resolve them, then:
git add .
git rebase --continue
```

### Before Submitting

```bash
# Clean up commits
git rebase -i upstream/master

# Verify your changes
git diff upstream/master

# Force push (only if rebasing)
git push -f origin feature/your-feature-name
```

---

## 📤 Submitting Changes

### Pull Request Process

1. **Push to Your Fork**
   ```bash
   git push origin feature/your-feature-name
   ```

2. **Create Pull Request**
   - Go to your fork on GitHub
   - Click "Compare & pull request"
   - Fill in the PR template:

   ```markdown
   ## Description
   Brief description of changes

   ## Type of Change
   - [ ] New feature
   - [ ] Bug fix
   - [ ] Breaking change
   - [ ] Documentation update

   ## Checklist
   - [ ] Code follows project style guidelines
   - [ ] Self-review completed
   - [ ] Comments added for complex logic
   - [ ] Documentation updated
   - [ ] No new warnings generated
   - [ ] Changes tested locally

   ## Related Issues
   Fixes #(issue number)

   ## Testing Performed
   Description of testing done
   ```

3. **Address Feedback**
   - Review comments from maintainers
   - Make requested changes
   - Commit with descriptive messages
   - Push updates to your branch

4. **Merge**
   - Maintainers will merge when approved
   - Your fork will be synced automatically

### PR Guidelines

- **One feature per PR**: Keep PRs focused and manageable
- **Clear description**: Explain what and why, not just what
- **Reasonable size**: Aim for <400 lines of changed code
- **No merge conflicts**: Rebase before submitting
- **Pass all checks**: All CI/CD checks must pass

---

## 🧪 Testing

### Manual Testing

1. **Build the Solution**
   ```bash
   Ctrl+Shift+B
   ```

2. **Run Demo Application**
   - Press __F5__ to debug
   - Test the control with various settings
   - Verify visual appearance

3. **Test with Different Values**
   - Minimum and maximum values
   - Edge cases and boundary conditions
   - Various needle styles
   - Different color configurations

### Areas to Test

**For Needle Styles**:
- ✅ Renders correctly at various angles
- ✅ Shadow and highlight effects display properly
- ✅ No rendering artifacts or clipping
- ✅ Hub renders correctly

**For Properties**:
- ✅ Setting property invalidates control
- ✅ Values are clamped correctly
- ✅ Color changes apply immediately
- ✅ Font scaling works at different sizes

**For Performance**:
- ✅ No lag when updating Value rapidly
- ✅ No memory leaks over time
- ✅ Rendering completes within reasonable time

---

## 📚 Documentation

### Code Documentation

- Add XML comments to all public members
- Document parameters and return values
- Include usage examples for complex features
- Update README.md for user-facing features

### Updating README.md

1. **For New Needle Style**:
   - Add to `NeedleStyle` enum documentation
   - Include usage example
   - Add to appropriate collection section

2. **For New Property**:
   - Add to relevant category table
   - Include description and default value
   - Provide usage example

3. **For API Changes**:
   - Update API reference section
   - Include breaking change notices
   - Provide migration guidance

---

## 🐛 Reporting Issues

### Issue Template

When reporting a bug, include:

```markdown
## Description
Clear description of the issue

## Steps to Reproduce
1. Step one
2. Step two
3. ...

## Expected Behavior
What should happen

## Actual Behavior
What actually happens

## Environment
- Visual Studio: [version]
- .NET Framework: 4.8
- Windows: [version]

## Screenshots
If applicable, add screenshots

## Additional Context
Any other relevant information
```

### Issue Labels

- `bug` - Something isn't working
- `feature` - New functionality request
- `enhancement` - Improvement to existing feature
- `documentation` - Documentation issue
- `performance` - Performance optimization
- `help wanted` - Needs assistance

---

## 💡 Feature Requests

### Request Template

```markdown
## Description
Clear description of desired feature

## Motivation
Why this feature is needed

## Proposed Solution
How you envision this working

## Alternative Approaches
Other possible implementations

## Additional Context
Related issues or discussions
```

### Acceptance Criteria

Features should:
- ✅ Solve a real problem
- ✅ Align with project scope
- ✅ Maintain backward compatibility
- ✅ Not introduce significant performance costs
- ✅ Be properly documented

---

## 🎯 Development Tips

### Extending Needle Styles

To add a new needle style:

1. **Add Enum Value**
   ```csharp
   public enum NeedleStyle
   {
       // ... existing styles ...
       MyNewStyle  // Add here
   }
   ```

2. **Implement Drawing Method**
   ```csharp
   private void DrawNeedle_MyNewStyle(Graphics g, Rectangle rect)
   {
       float r = rect.Width * 0.5f;
       var c = new PointF(rect.Left + r, rect.Top + r);
       float a = MapValueToAngle(Value);
       
       // Your drawing code here
       
       DrawNeedleHub(g, c, r);
   }
   ```

3. **Add Case to Switch**
   ```csharp
   case NeedleStyle.MyNewStyle:
       DrawNeedle_MyNewStyle(g, rect);
       break;
   ```

4. **Document**
   - Add to README.md Needle Styles section
   - Include usage example
   - Document any new properties

### Adding Properties

To add a new customizable property:

1. **Add Backing Field**
   ```csharp
   private float _myProperty = 0.5f;
   ```

2. **Add Public Property**
   ```csharp
   [Category("7. Needle")]
   [Description("Description of property")]
   public float MyProperty
   {
       get => _myProperty;
       set { _myProperty = value; Invalidate(); }
   }
   ```

3. **Use in Drawing Methods**
   ```csharp
   float value = r * MyProperty; // Use in calculations
   ```

4. **Update Designer**
   - Set default value in Form1.Designer.cs
   - Test in designer and at runtime

---

## 📞 Questions or Need Help?

- **GitHub Issues**: Create an issue for discussion
- **Code Review**: Ask questions in pull request comments
- **Documentation**: Check README.md and inline comments

---

## ✅ Checklist Before Submitting

- [ ] Code follows `.editorconfig` style guidelines
- [ ] Private fields use `_fieldName` convention
- [ ] Public properties use `PascalCase`
- [ ] XML documentation added to public members
- [ ] No compiler warnings or errors
- [ ] Solution builds successfully
- [ ] Changes tested manually
- [ ] Git history is clean
- [ ] Commit messages are descriptive
- [ ] PR description is clear and complete

---

Thank you for contributing to SpeedoMeter! 🎉

Your contributions help make this project better for everyone.

**Happy coding!**
