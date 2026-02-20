# Bank Credit Simulator (Unity 3D)

Bank Credit Simulator is a 3D Unity project that simulates a simplified banking loan approval system.
The player logs into the system, interacts with a bank employee (NPC), submits a loan request, and receives a decision based on their financial capacity.
This project combines gameplay mechanics with financial logic and system architecture principles.

# How the Project Works

## Login System

The application starts with a login screen.
The user must enter:
- Login
- Password

User data is stored in a JSON file inside the project.
Example structure:
```json
{
  "users": [
    {
      "login": "admin",
      "password": "admin123",
      "income": 12000
    }
  ]
}
```
If credentials are correct:
A session is created
User income is loaded
The main game scene is opened

## Controls
In Game:

WSAD → Move 

Mouse → Look around

## Session Management

The project uses a Singleton pattern:

SessionManager

Responsibilities:

- Stores logged user

- Stores current income

- Persists between scenes

- Resets on logout

This ensures clean separation between:

- Authentication

- Gameplay logic

- Financial calculations

## Loan System Logic

The player enters the requested loan amount.

The system checks:

- Is the number valid?

- Is it greater than zero?

- Is the user logged in?

- Does the user have income?

Then it calculates maximum loan capacity:

Loan Formula:

Maximum monthly installment = 40% of income

Loan duration = 60 months (5 years)

If requested amount ≤ maxLoan → Loan Approved
Otherwise → Loan Rejected

## NPC Interaction

The player interacts with the Banker NPC.

During conversation:

- The character turns toward the banker

- Loan request system is activated

- Keyboard input controls dialog progression

Controls during conversation:

- ENTER → confirm / continue

- SPACE → exit conversation
