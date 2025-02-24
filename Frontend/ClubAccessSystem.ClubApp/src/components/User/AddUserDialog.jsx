import { Dialog, DialogPanel, DialogTitle } from "@headlessui/react";
import { useState } from "react";
import api from "../../data/api";


export default function AddUserDialog({ isOpen, setIsOpen, onUserCreated }) {
    const [formData, setFormData] = useState({
        nombre: "",
        email: "",
        password: "",
        rolId: 0,
      });
      const [errors, setErrors] = useState({});
    
      // Validar el formulario
      const validateForm = () => {
        const newErrors = {};
    
        if (!formData.nombre.trim()) {
          newErrors.nombre = "El nombre es requerido";
        }
        if (!formData.email.trim()) {
          newErrors.email = "El email es requerido";
        } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
          newErrors.email = "El email no es válido";
        }
        if (!formData.password.trim()) {
          newErrors.password = "La contraseña es requerida";
        } else if (formData.password.length < 8) {
          newErrors.password = "La contraseña debe tener al menos 8 caracteres";
        }
        if (formData.rolId <= 0) {
          newErrors.rolId = "El rol es requerido y debe ser mayor a 0";
        }
    
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0; // Retorna true si no hay errores
      };
    
      // Manejar cambios en los campos del formulario
      const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
          ...formData,
          [name]: value,
        });
      };
    
      // Manejar el envío del formulario
      const handleSubmit = async (e) => {
        e.preventDefault();
    
        if (!validateForm()) {
          return; // Detener el envío si hay errores
        }
    
        try {
          const response = await api.post("/Usuario/createUsuario", formData);
          if (response.data.success) {
            onUserCreated(response.data.data); // Notificar que se creó un usuario
            setIsOpen(false); // Cerrar el diálogo
            setFormData({ nombre: "", email: "", password: "", rolId: 0 }); // Limpiar el formulario
            setErrors({}); // Limpiar errores
          }
        } catch (error) {
          console.error("Error al crear el usuario:", error);
          setErrors({ submit: "Error al crear el usuario. Inténtalo de nuevo." });
        }
    };

  return (
    <Dialog open={isOpen} onClose={() => setIsOpen(false)} className="relative z-50">
      <div className="fixed inset-0 bg-black/30" aria-hidden="true" />
      <div className="fixed inset-0 flex items-center justify-center p-4">
        <DialogPanel className="w-full max-w-md rounded-lg bg-white p-6">
          <DialogTitle className="text-lg font-semibold text-gray-900">
            Crear Usuario
          </DialogTitle>
          <form onSubmit={handleSubmit} className="mt-4 space-y-4">

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

            <div>
              <label className="block text-sm font-medium text-gray-700">Email</label>
              <input
                type="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                className={`mt-1 block w-full rounded-md border ${
                  errors.email ? "border-red-500" : "border-gray-300"
                } shadow-sm focus:border-blue-500 focus:ring-blue-500`}
              />
              {errors.email && (
                <p className="mt-1 text-sm text-red-500">{errors.email}</p>
              )}
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700">Contraseña</label>
              <input
                type="password"
                name="password"
                value={formData.password}
                onChange={handleChange}
                className={`mt-1 block w-full rounded-md border ${
                  errors.password ? "border-red-500" : "border-gray-300"
                } shadow-sm focus:border-blue-500 focus:ring-blue-500`}
              />
              {errors.password && (
                <p className="mt-1 text-sm text-red-500">{errors.password}</p>
              )}
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700">Rol</label>
              <input
                type="number"
                name="rolId"
                value={formData.rolId}
                onChange={handleChange}
                min="1"
                className={`mt-1 block w-full rounded-md border ${
                  errors.rolId ? "border-red-500" : "border-gray-300"
                } shadow-sm focus:border-blue-500 focus:ring-blue-500`}
              />
              {errors.rolId && (
                <p className="mt-1 text-sm text-red-500">{errors.rolId}</p>
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
                Crear Usuario
              </button>
            </div>
          </form>
        </DialogPanel>
      </div>
    </Dialog>
  );
}