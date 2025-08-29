# Member Management API

A simple API for managing **Members**, **Reward Points**, and **Coupon Redemptions**, built using **ASP.NET Core 8**, **Entity Framework Core**, and **MySQL**.

---

## Features
- **Member Management**: Registration, OTP verification, and profile fetching.
- **Points Management**: Adding and retrieving reward points.
- **Coupon Redemption**: Redeeming points for coupons and viewing redeemed coupons.
- **JWT Authentication**: Secure API endpoints.
- **Swagger UI**: API documentation and testing.

---

## API Endpoints

### **Member Endpoints**
| Method | Endpoint                 |Description                     |
|--------|--------------------------|--------------------------------|
| `POST` | `/api/Member/register`   | Register a new member.         |
| `POST` | `/api/Member/verify-otp` | Verify OTP for member account. |
| `GET`  | `/api/Member/profile/{id}`| Fetch member profile by ID.   |

---

### **Points Endpoints**
| Method | Endpoint                                 Description             |
|--------|------------------------- |-----------------------------------------|
| `POST` | `/api/Points/add`        | Add reward points to a member account.  |
| `GET`  | `/api/Points/{memberId}` | Get all points for a specific member.   |

---

### **Redemption Endpoints**
| Method | Endpoint                                    | Description                        |
|--------|----------|------------------------------------------------------------------------                 
| `POST` | `/api/Redemption/redeem`                    | Redeem points for a coupon.        |
| `GET`  | `/api/Redemption/member/{memberId}/coupons` | Get redeemed coupons for a member. |

---

## Authentication
All endpoints are secured with **JWT Bearer Authentication**.  
Use the **Authorize** button in Swagger UI and provide a valid JWT token.

---

## Running the Project
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/member-management-api.git
   cd member-management-api
2. Update appsettings.json with your MySQL credentials and JWT settings.
3. Run database migrations:
   dotnet ef database update
4. Start the application:
   dotnet run
5.Open Swagger UI:
  https://localhost:5256/swagger/


**Project Structure**

Models – Entity models for Members, Points, Coupons.

Controllers – API endpoints logic.

DTOs – Data Transfer Objects.

wwwroot – Static HTML for SPA.

** Frontend (SPA)**

HTML files served from wwwroot directory.

Includes features like:

Member registration.

Points display.

Coupon redemption with remaining points display.

**POSTMAN COLLECTION LINK**
https://www.postman.com/anjaliray5973187-5761672/workspace/anjali-yadav-s-workspace/collection/48018407-a7ae08c2-3125-4092-8e34-863da3418e19?action=share&creator=48018407&active-environment=48018407-0e4cf8fc-aa04-4cbe-8d42-0fdb43747ea5

