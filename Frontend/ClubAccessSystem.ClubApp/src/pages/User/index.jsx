"use client";

import { useEffect, useState } from "react";
import UserTable from "../../components/User/UserTable";
import AddUserDialog from "../../components/User/AddUserDialog";
import { PlusIcon } from "@heroicons/react/24/outline";
import api from "../../data/api";

export default function Users() {
  const [isOpen, setIsOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState("");
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);


  useEffect(() =>{
    const fectchUsers = async () =>{
      try {
        const response = await api.get("/Usuario/getAllUsuario");
        if(response.data.success){
          setUsers(response.data.data);
        }
        
      } catch (error) {
        console.error("Error fetching users:", error);
      }
      finally{
        setLoading(false);
      }
    }

    fectchUsers();

  }, []);



  // Función para agregar un nuevo usuario
  const handleUserCreated = (newUser) => {
    setUsers([...users, newUser]); // Agregar el nuevo usuario a la lista
  };

  // Filtrar usuarios basados en la búsqueda
  const filteredUsers = users.filter((user) =>
    user.nombre.toLowerCase().includes(searchQuery.toLowerCase()
    )
    
  );

  return (
    
    <div className="p-6">
      <div className="flex justify-end mb-6">
        <button
          onClick={() => setIsOpen(true)}
          className="flex items-center bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition duration-200"
        >
          <PlusIcon className="w-5 h-5 mr-2" />
          Agregar Usuario
        </button>
      </div>
      {loading ? (
        <p>Cargando usuarios...</p>
      ) : (
        <UserTable users={filteredUsers} searchQuery={searchQuery} setSearchQuery={setSearchQuery} />
      )}

      <AddUserDialog
        isOpen={isOpen}
        setIsOpen={setIsOpen}
        onUserCreated={handleUserCreated}
      />


    </div>



  );
}