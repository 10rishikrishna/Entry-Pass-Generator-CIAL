# CIAL Entry Pass Generator 🛂

> A Windows desktop application for generating and managing digital entry passes with API-based authentication, pass approval, and digital signature support.

## 📌 Overview

**CIAL Entry Pass Generator** is a C# Windows Forms application designed to simplify the process of creating and submitting entry passes for approval.

The application communicates with an ASP.NET Web API backend to handle authentication, password management, pass submission, and approval-related data.

The system is designed to make the entry-pass process more **organized, secure, and paperless**.

---

## ✨ Features

- 🔐 User registration and login
- 🔑 Secure password generation
- 🔄 Change password functionality
- 📝 Create entry passes
- 📤 Submit passes for approval
- ⏳ Track pass status
- 👤 Store labor/employee details
- 📸 Support for labor images using Base64
- ✍️ Digital signature support
- ✅ Approval information tracking
- ❌ Rejection reason tracking
- 📥 Download status tracking
- 🌐 REST API communication
- ⚡ Asynchronous API requests
- 🛡️ Error handling and connection status messages

---

## 🏗️ Architecture

The application follows a simple client-server architecture:

```text
┌─────────────────────────────┐
│     C# Windows Forms        │
│          Client             │
│                             │
│  • Login / Register         │
│  • Pass Generation          │
│  • Pass Submission          │
│  • User Interface           │
└──────────────┬──────────────┘
               │
               │ HTTP / JSON
               ▼
┌─────────────────────────────┐
│      ASP.NET Web API        │
│                             │
│  /api/auth/login            │
│  /api/auth/register         │
│  /api/auth/change-password  │
│  /api/auth/generate-password│
│  /api/passes                │
└─────────────────────────────┘
