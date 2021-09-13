# Terraform root
terraform {
  required_providers {
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = ">= 2.4.1"
    }
    helm = {
      source = "hashicorp/helm"
      version = ">= 2.3.0"
    }
    tls = {
      source  = "hashicorp/tls"
      version = ">= 3.1.0"
    }
    local = {
      source  = "hashicorp/local"
      version = ">= 2.1.0"
    }
    random = {
      source = "hashicorp/random"
      version = ">= 3.1.0"
    }
  }
}
provider "kubernetes" {
  config_path = "C:\\Users\\lesar\\.kube\\config"
}
provider "random" {
  # Configuration options
}
provider "helm" {
  kubernetes {
    config_path = "C:\\Users\\lesar\\.kube\\config"
  }
}