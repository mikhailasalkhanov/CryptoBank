resource "azurerm_resource_group" "resources" {
  name     = "resources"
  location = "West Europe"
}

resource "azurerm_virtual_network" "network" {
  name                = "network"
  location            = azurerm_resource_group.resources.location
  resource_group_name = azurerm_resource_group.resources.name
  address_space       = ["10.0.0.0/16"]
  
}

resource "azurerm_subnet" "subnet" {
  name                 = "subnet"
  resource_group_name  = azurerm_resource_group.resources.name
  virtual_network_name = azurerm_virtual_network.network.name
  address_prefixes     = ["10.0.1.0/24"]
}

resource "azurerm_network_interface" "main" {
  name                = "main-nic"
  location            = azurerm_resource_group.resources.location
  resource_group_name = azurerm_resource_group.resources.name

  ip_configuration {
    name                          = "testconfiguration1"
    subnet_id                     = azurerm_subnet.subnet.id
    private_ip_address_allocation = "Dynamic"
  }
}

resource "azurerm_virtual_machine" "main" {
  name                  = "main-vm"
  location              = azurerm_resource_group.resources.location
  resource_group_name   = azurerm_resource_group.resources.name
  network_interface_ids = [azurerm_network_interface.main.id]
  vm_size               = "Standard_DS1_v2"
  delete_os_disk_on_termination = true
  delete_data_disks_on_termination = true

  storage_image_reference {
    publisher = "Canonical"
    offer     = "0001-com-ubuntu-server-jammy"
    sku       = "22_04-lts"
    version   = "latest"
  }
  storage_os_disk {
    name              = "myosdisk1"
    caching           = "ReadWrite"
    create_option     = "FromImage"
    managed_disk_type = "Standard_LRS"
  }
  os_profile {
    computer_name  = "main"
    admin_username = "cryptobank-admin"
    admin_password = "Password1234!"
  }
  os_profile_linux_config {
    disable_password_authentication = false
  }
}