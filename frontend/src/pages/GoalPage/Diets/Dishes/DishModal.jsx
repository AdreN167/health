import { Modal, Box, Grid2, Typography, Stack } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import StorageContext from "../../../../store/StorageContext";
import DishCard from "./DishCard";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "80%",
  maxHeight: "80vh",
  overflowY: "auto",
  bgcolor: "background.paper",
  boxShadow: 24,
  p: 4,
};

const defaultWeight = 100;

const DishModal = ({ open, dishesInUse, handleClose, onDishClick }) => {
  const { dishes, fetchDishes } = useContext(StorageContext);
  const [dishesFromStorage, setDishesFromStorage] = useState([]);
  const [isNothingDishes, setIsNothingDishes] = useState(false);

  useEffect(() => {
    if (dishes.length === 0) {
      const get = async () => {
        setDishesFromStorage(await fetchDishes());
      };
      get();
      return;
    }
    setDishesFromStorage(dishes);
  }, [dishes, dishesFromStorage]);

  useEffect(() => {
    setIsNothingDishes(dishes.length === dishesInUse.length);
  }, [dishesInUse]);

  const handleClick = (id) => {
    const dish = dishesFromStorage.find((ex) => ex.id === id);
    console.log({ ...dish, weight: defaultWeight });
    onDishClick({ ...dish, weight: defaultWeight });
  };

  return (
    <Modal open={open} onClose={handleClose}>
      <Box sx={style}>
        {!isNothingDishes ? (
          <Stack container spacing={4} alignItems="center">
            {dishesFromStorage &&
              dishesFromStorage.map((dish) => {
                if (!dishesInUse.includes(dish.id)) {
                  return (
                    <DishCard
                      key={dish.id}
                      id={dish.id}
                      name={dish.name}
                      description={dish.description}
                      calories={dish.calories}
                      proteins={dish.proteins}
                      fats={dish.fats}
                      carbohydrates={dish.carbohydrates}
                      products={dish.products}
                      imageUrl={dish.imageUrl}
                      onClick={handleClick}
                    />
                  );
                }
              })}
          </Stack>
        ) : (
          <Typography>К сожалению, пока больше нет доступных блюд</Typography>
        )}
      </Box>
    </Modal>
  );
};

export default DishModal;
