import { Dialog, DialogPanel, DialogTitle } from "@headlessui/react";
import { useState } from "react";
import api from "../../data/api";


export default function AddClientDialog({ isOpen, setIsOpen, onClientCreated }) {
  const [formData, setFormData] = useState({
    nombre: "",
    contacto: "",
    tipoCliente: 0,
  });
  const [errors, setErrors] = useState({});

  const validateForm = () => {
    const newErrors = {};

    if (!formData.nombre.trim()) {
      newErrors.nombre = "El nombre es requerido";
    }
    if (!formData.contacto.trim()) {
      newErrors.contacto = "El contacto es requerido";
    } else if (!/\S+@\S+\.\S+/.test(formData.contacto)) {
      newErrors.contacto = "El contacto no es válido";
    }
    
    if (formData.tipoCliente <= 0) {
      newErrors.tipoCliente = "El Tipo cliente es requerido y debe ser mayor a 0";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: name === "clienteId" ? parseInt(value) : value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    try {
      const response = await api.post("/Clientes/createCliente", formData);
      
      if (response.status === 200) {
        // Enviamos el mismo formData como nuevo cliente, ya que contiene la estructura correcta
        const newClient = {
          ...formData,
          clienteId: response.data.data // Asumiendo que el API devuelve el ID del nuevo cliente
        };
        
        onClientCreated(newClient);
        
        // Limpiar el formulario
        setFormData({
          nombre: "",
          contacto: "",
          tipoCliente: 0,
        });
        
        setErrors({});
        setIsOpen(false);
      }
    } catch (error) {
      console.error("Error al crear el cliente:", error);
      setErrors({
        submit: "Error al crear el cliente. Por favor, inténtalo de nuevo.",
      });
    }
  };

  return (
    <Dialog open={isOpen} onClose={() => setIsOpen(false)} className="relative z-50">
      <div className="fixed inset-0 bg-black/30" aria-hidden="true" />
      <div className="fixed inset-0 flex items-center justify-center p-4">
        <DialogPanel className="w-full max-w-md rounded-lg bg-white p-6">
          <DialogTitle className="text-lg font-semibold text-gray-900">
            Crear Cliente
          </DialogTitle>
          <form onSubmit={handleSubmit} className="mt-4 space-y-4">
            {/* Campo Nombre */}
            <div>
              <label className="block text-sm font-medium text-gray-700">Nombre</label>
              <input
                type="text"
                name="nombre"
                value={formData.nombre}
                onChange={handleChange}
                className={`mt-1 block w-full rounded-md border ${
                  errors.nombre ? "border-red-500" : "border-gray-300"
                } shadow-sm focus:border-blue-500 focus:ring-blue-500`}
              />
              {errors.nombre && (
                <p className="mt-1 text-sm text-red-500">{errors.nombre}</p>
              )}
            </div>

            {/* Campo Contacto */}
            <div>
              <label className="block text-sm font-medium text-gray-700">Contacto</label>
              <input
                type="text"
                name="contacto"
                value={formData.contacto}
                onChange={handleChange}
                className={`mt-1 block w-full rounded-md border ${
                  errors.contacto ? "border-red-500" : "border-gray-300"
                } shadow-sm focus:border-blue-500 focus:ring-blue-500`}
              />
              {errors.contacto && (
                <p className="mt-1 text-sm text-red-500">{errors.contacto}</p>
              )}
            </div>

            

            {/* Campo Tipo cliente */}
            <div>
              <label className="block text-sm font-medium text-gray-700">Tipo Cliente</label>
              <input
                type="number"
                name="tipoCliente"
                value={formData.tipoCliente}
                onChange={handleChange}
                min="1"
                className={`mt-1 block w-full rounded-md border ${
                  errors.tipoCliente ? "border-red-500" : "border-gray-300"
                } shadow-sm focus:border-blue-500 focus:ring-blue-500`}
              />
              {errors.tipoCliente && (
                <p className="mt-1 text-sm text-red-500">{errors.tipoCliente}</p>
              )}
            </div>

            {/* Mensaje de error general */}
            {errors.submit && (
              <p className="mt-2 text-sm text-red-500">{errors.submit}</p>
            )}

            {/* Botones del formulario */}
            <div className="flex justify-end space-x-4">
              <button
                type="button"
                onClick={() => setIsOpen(false)}
                className="px-4 py-2 text-sm font-medium text-gray-700 bg-gray-100 rounded-lg hover:bg-gray-200"
              >
                Cancelar
              </button>
              <button
                type="submit"
                className="px-4 py-2 text-sm font-medium text-white bg-blue-600 rounded-lg hover:bg-blue-700"
              >
                Crear Cliente
              </button>
            </div>
          </form>
        </DialogPanel>
      </div>
    </Dialog>
  );
}