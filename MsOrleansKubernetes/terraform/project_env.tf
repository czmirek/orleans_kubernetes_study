locals {
    orleans_env = {
        "ORLEANS_DASHBOARD_USERNAME" = "orleans"
        "ORLEANS_DASHBOARD_HOST" = "*"
        "ORLEANS_DASHBOARD_PORT" = "8080"
    }

    orleans_secure_env = {
        "ORLEANS_DASHBOARD_PASSWORD" = random_password.orleans_dashboard_password.result
    }
}