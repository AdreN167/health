import { createContext, useState } from "react";

const DietsContext = createContext();

export const DietsProvider = ({ children }) => {
  const [diets, setDiets] = useState([]);

  const handleDeleteDiet = (id) => {
    setDiets(diets.filter((w) => w.id !== id));
  };

  const handleAddDiet = (diet) => {
    setDiets((prev) => [...prev, diet]);
  };

  const handleUpdateDietName = (id, newName) => {
    setDiets(diets.map((w) => (w.id === id ? { ...w, name: newName } : w)));
  };

  const handleReplaceProductsToDiet = (dietId, products) => {
    setDiets(
      diets.map((w) => (w.id === dietId ? { ...w, products: products } : w))
    );
  };

  const handleReplaceDishesToDiet = (dietId, dishes) => {
    setDiets(
      diets.map((w) => (w.id === dietId ? { ...w, dishes: dishes } : w))
    );
  };

  const handleGetById = (id) => diets.find((w) => w.id === id);

  return (
    <DietsContext.Provider
      value={{
        diets,
        init: setDiets,
        del: handleDeleteDiet,
        create: handleAddDiet,
        addProducts: handleReplaceProductsToDiet,
        addDishes: handleReplaceDishesToDiet,
        updateName: handleUpdateDietName,
        getById: handleGetById,
      }}
    >
      {children}
    </DietsContext.Provider>
  );
};

export default DietsContext;
