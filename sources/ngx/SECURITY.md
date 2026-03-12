# Security Information for Kaltura NGX Client Library

This document provides important information about the Kaltura NGX (Angular) client library's security posture, versioning, and upgrade guidance.

## Overview

The Kaltura NGX client library is an auto-generated Angular client that provides a typed interface to the Kaltura API. It is designed to facilitate video upload, playback, and other Kaltura platform operations within Angular applications.

## Versioning and Deployment

### Current Version
- **Latest documented version**: Venus-22.13 (API schema version)
- **Client library version**: See `package.json` for the current library version

### How Versioning Works
The Kaltura NGX client library is:
- **Automatically built and deployed** with each Kaltura server-side deployment
- **Regenerated** to match the latest Kaltura API schema
- **Schema-driven**: Only the API/service names and types evolve to reflect new Kaltura capabilities

## Security Posture

### Client Infrastructure Stability
The NGX client infrastructure:
- Has been **stable for 6+ years**
- Undergoes **no security-relevant infrastructure changes** during regeneration
- Only API/service definitions are updated to match server schema changes

### What Upgrading Does and Does Not Do
| Upgrading the NGX Client Library... | Result |
|-------------------------------------|--------|
| Updates API/service definitions | ✅ Yes |
| Provides new Kaltura API capabilities | ✅ Yes |
| Improves client-side security posture | ❌ No |
| Addresses Angular framework vulnerabilities | ❌ No |
| Addresses Node.js runtime vulnerabilities | ❌ No |

**Key Point**: Upgrading the NGX client library will **not** change its security posture because there have been no infrastructure-level client changes that would impact security. The client is fully backward compatible.

## Addressing Security Concerns

### Client Library vs. Runtime Environment
It is important to distinguish between:
1. **Client library code** (this repository): Auto-generated API bindings
2. **Runtime environment** (your application's Node.js/Angular version): Where security patches are applied

### Node.js Vulnerabilities
If you have concerns about Node.js vulnerabilities (e.g., CVEs affecting Node.js 8.x–18.x or unpatched 20.x/22.x/24.x/25.x):
- These are **runtime environment concerns**, not client library concerns
- **Action required**: Update your application's Node.js version to a patched release
- The NGX client library itself does not bundle Node.js and is not affected by Node.js runtime vulnerabilities

### Angular Framework Vulnerabilities
If you require a client library built on a newer Angular version:
- This is considered a **feature request**, not a support fix
- The current client is built on Angular 6.x for broad compatibility
- Please submit a feature request through the appropriate channels

## Upgrade Guidance

### When to Upgrade
You should consider upgrading the NGX client library when:
- You need access to new Kaltura API features or services
- Your Kaltura server has been updated and you want matching API definitions

### When Upgrading Won't Help
Upgrading the NGX client library will **not**:
- Fix Node.js vulnerabilities (update your Node.js runtime instead)
- Fix Angular framework vulnerabilities (this requires a new client build on a newer Angular version)
- Improve security if you're already on a recent library version

### How to Upgrade
See the [README.md](README.md) for instructions on:
1. Building the library from source
2. Installing the built package in your application

## Feature Requests

For requests that require building the NGX client on a newer Angular version or other framework changes:
1. Submit a feature request via [GitHub Issues](https://github.com/kaltura/clients-generator/issues)
2. Clearly describe your requirements and use case
3. Such requests will be prioritized as enhancements

## Reporting Security Issues

If you discover a security vulnerability in the Kaltura NGX client library itself (not the runtime environment), please report it responsibly:
- **Do not** open a public issue for security vulnerabilities
- Contact Kaltura security team through appropriate channels
- See [Kaltura's security policy](https://corp.kaltura.com/security/) for more information

## Summary

| Concern | Resolution |
|---------|------------|
| Latest NGX library version | Venus-22.13 (regenerated with each server deployment) |
| Need new API features | Upgrade the NGX client library |
| Node.js vulnerabilities | Update your application's Node.js runtime |
| Angular framework vulnerabilities | Submit a feature request for newer Angular build |
| General security improvements | No action needed; client infrastructure unchanged for 6+ years |

---
*This document reflects the current state of the Kaltura NGX client library as maintained in the [kaltura/clients-generator](https://github.com/kaltura/clients-generator) repository.*
