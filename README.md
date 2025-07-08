# BB5 — Blazor Components for Bootstrap 5

**BB5** is a powerful set of Blazor components built on top of **Bootstrap 5**,
designed to help you build fast, clean, and modern UIs with minimal effort.

Whether you're crafting individual controls or assembling full-featured
data-driven screens, BB5 gives you the tools you need—out of the box.

## 🚀 Why BB5?

Building Blazor apps should be quick, consistent, and scalable. BB5 solves
the problem of slow and repetitive UI development by providing:

* **Multiple levels of abstraction**: From low-level UI building blocks to
  high-level smart components.
* **Out-of-the-box UX patterns**: Data grids, record forms, action buttons,
  and layouts that just work.
* **Minimal boilerplate**: Plug in your model, hook up a data source, and
  you're done.

## 🧰 Use Case: View and edit users

```csharp
// 1. Define a simple user model. Use data annotations.
public class User
{
    [Key]
    public string Name { get; set; }
    
    [DisplayName("Email Address")]
    public string Email { get; set; }
}
```

```razor
// 2. Create list of users 
Users = new List<User> {
    new User { Name = "Alice", Email = "alice@example.com" },
    new User { Name = "Bob", Email = "bob@example.com" }
};

// 3. Use BB5 components to display and edit users.
// 3.1. Table uses reflection to render columns
<Table Items="@users"
       OnActivate="@(obj => User = obj as User)" />
// 3.2. Form uses reflection to generate fields
<Form Object="@User" />
```

## 🧩 What’s Inside?

### ✅ Core Components

Essential Blazor elements styled with Bootstrap 5: buttons, inputs, layout
grids, modals, tabs, and more. Fully customisable and composable.

### 📄 Form

Generate entire forms from a model definition. Supports validation, layout
presets, custom field components, and save/cancel logic with hooks.

### 📊 Table

Dynamic, responsive tables with built-in pagination, sorting, inline actions,
and optional editing modes. Just bind a list—BB5 handles the rest.

## 🛠️ Designed for Developers

BB5 gives you the control you want *when you need it*, and hides
the complexity when you don't. Ideal for internal tools, admin panels,
dashboards, and any Blazor project where delivery speed matters.

## 📚 Docs & Demos

Explore the [documentation](./BB5.Docs/Overview.md) for detailed guides.

Check out the samples folder to get started fast.

## 🧪 Still Evolving

BB5 is under active development. Contributions, feedback, and ideas are welcome.

## 📄 Licensing

BB5 is a commercial product with the following licensing terms:

* **Free** for project contributors (see CONTRIBUTING.md).
* **\$99/year** per individual developer ([purchase](https://buy.stripe.com/7sYdR9aco30I1t14Dk0Ba01))
* **\$999/year** for company-wide use ([purchase](https://buy.stripe.com/4gM9AT0BOata0oXd9Q0Ba00))

Individuals and companies are entitled for free trial use for 30 days.
