terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.85.0"
    }
  }

  required_version = ">= 0.15"
}

provider "azurerm" {
  features {}
}
