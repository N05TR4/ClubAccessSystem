
"use client";

import { useEffect, useState } from "react";
import ClientTable from "../../components/Client/ClientTable";
import AddClientDialog from "../../components/Client/AddClientDialog";
import { PlusIcon } from "@heroicons/react/24/outline";
import api from "../../data/api";

export default function Client() {
  const [isOpen, setIsOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState("");
  const [clients, setClients] = useState([]);
  const [loading, setLoading] = useState(true);

  // Obtener clientes desde la API
  useEffect(() => {
    const fetchClients = async () => {
      try {
        const response = await api.get("/Clientes/getAllCliente");
        if (response.data.success) {
            setClients(response.data.data);
        }
      } catch (error) {
        console.error("Error fetching clients:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchClients();
  }, []);

  // Función para manejar la creación de un nuevo cliente
 
  const handleClientCreated = (newClient) => {
    if (newClient && newClient.nombre) {
      setClients(prevClient => [...prevClient, newClient]);
    }
  };

  // Filtrar clientes basados en la búsqueda
  const filteredClients = clients.filter((client) =>
    client.nombre?.toLowerCase().includes(searchQuery.toLowerCase())
  );

  return (
    <div className="p-6">
        {/* Título de la sección */}
        <div className="flex justify-between items-center mb-6">
            <h1 className="text-2xl font-semibold text-gray-800">Clientes</h1>
            {/* Botón para abrir el diálogo */}
            <button
            onClick={() => setIsOpen(true)}
            className="flex items-center bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition duration-200"
            >
            <PlusIcon className="w-5 h-5 mr-2" />
            Agregar Cliente
            </button>
        </div>

        {/* Componente de la tabla */}
        {loading ? (
            <p>Cargando Clientes...</p>
        ) : (
            <ClientTable clients={filteredClients} searchQuery={searchQuery} setSearchQuery={setSearchQuery} />
        )}

        {/* Componente del diálogo */}
        <AddClientDialog
            isOpen={isOpen}
            setIsOpen={setIsOpen}
            onClientCreated={handleClientCreated}
        />
    </div>
  );
}