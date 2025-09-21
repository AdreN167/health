import { Modal, Box, Grid2, Typography, Stack } from "@mui/material";
import DishCard from "../Dishes/DishCard";
import { useEffect, useState } from "react";
import FoodCard from "./FoodCard";
import InnerShadowBox from "../../../../components/InnerShadowBox/InnerShadowBox";
import StartButton from "../../../../components/StartButton/StartButton";

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

const defaultKbju = {
  k: 0,
  b: 0,
  j: 0,
  u: 0,
};

const FoodModal = ({ open, dishes, products, handleClose, onApplyMeal }) => {
  const [selectedDishes, setSelectedDishes] = useState([]);
  const [selectedProducts, setSelectedProducts] = useState([]);
  const [kbju, setKbju] = useState(defaultKbju);

  useEffect(() => {}, [selectedDishes, selectedProducts]);

  const handleApplyMeal = () => {
    onApplyMeal(selectedProducts, selectedDishes);
    setKbju(defaultKbju);
    setSelectedDishes([]);
    setSelectedProducts([]);
    handleClose();
  };

  const handleDishClick = (id) => {
    const dish = dishes.find((i) => i.id === id);
    let newList = [...selectedDishes];

    const newKbju = {
      ...kbju,
    };

    if (newList.includes(id)) {
      newKbju["k"] =
        newKbju["k"] - dish.calories < 0 ? 0 : newKbju["k"] - dish.calories;
      newKbju["b"] =
        newKbju["b"] - dish.proteins < 0 ? 0 : newKbju["b"] - dish.proteins;
      newKbju["j"] =
        newKbju["j"] - dish.fats < 0 ? 0 : newKbju["j"] - dish.fats;
      newKbju["u"] =
        newKbju["u"] - dish.carbohydrates < 0
          ? 0
          : newKbju["u"] - dish.carbohydrates;
      newList = newList.filter((i) => i !== id);
    } else {
      newKbju["k"] += dish.calories;
      newKbju["b"] += dish.proteins;
      newKbju["j"] += dish.fats;
      newKbju["u"] += dish.carbohydrates;
      newList.push(id);
    }
    setKbju(newKbju);

    setSelectedDishes(newList);
  };

  const handleProductClick = (id) => {
    const product = products.find((i) => i.id === id);
    let newList = [...selectedProducts];

    const newKbju = {
      ...kbju,
    };

    if (newList.includes(id)) {
      newKbju["k"] =
        newKbju["k"] - product.calories < 0
          ? 0
          : newKbju["k"] - product.calories;
      newKbju["b"] =
        newKbju["b"] - product.proteins < 0
          ? 0
          : newKbju["b"] - product.proteins;
      newKbju["j"] =
        newKbju["j"] - product.fats < 0 ? 0 : newKbju["j"] - product.fats;
      newKbju["u"] =
        newKbju["u"] - product.carbohydrates < 0
          ? 0
          : newKbju["u"] - product.carbohydrates;
      newList = newList.filter((i) => i !== id);
    } else {
      newKbju["k"] += product.calories;
      newKbju["b"] += product.proteins;
      newKbju["j"] += product.fats;
      newKbju["u"] += product.carbohydrates;
      newList.push(id);
    }

    setKbju(newKbju);

    setSelectedProducts(newList);
  };

  return (
    <Modal open={open} onClose={handleClose}>
      <Box sx={style}>
        <Stack direction="row">
          <Stack
            sx={{
              marginRight: 2,
              width: "70%",
            }}
          >
            <InnerShadowBox
              display="flex"
              flexDirection="row"
              gap={4}
              sx={{
                width: "100%",
                height: 298 + 8,
                borderRadius: 2,
              }}
            >
              {dishes &&
                dishes.map((dish) => (
                  <FoodCard
                    disabled
                    key={dish.id}
                    id={dish.id}
                    name={dish.name}
                    description={dish.description}
                    calories={dish.calories}
                    proteins={dish.proteins}
                    fats={dish.fats}
                    carbohydrates={dish.carbohydrates}
                    weight={dish.weight}
                    selected={selectedDishes.includes(dish.id)}
                    imageUrl={dish.imageUrl}
                    onSelect={handleDishClick}
                  />
                ))}
            </InnerShadowBox>
            <InnerShadowBox
              display="flex"
              flexDirection="row"
              gap={4}
              sx={{
                marginTop: 2,
                width: "100%",
                height: 298 + 8,
                borderRadius: 2,
              }}
            >
              {products &&
                products.map((product) => (
                  <FoodCard
                    disabled
                    key={product.id}
                    id={product.id}
                    name={product.name}
                    description={product.description}
                    calories={product.calories}
                    proteins={product.proteins}
                    fats={product.fats}
                    carbohydrates={product.carbohydrates}
                    weight={product.weight}
                    selected={selectedProducts.includes(product.id)}
                    imageUrl={product.imageUrl}
                    onSelect={handleProductClick}
                  />
                ))}
            </InnerShadowBox>
          </Stack>
          <Stack sx={{ marginLeft: 4, width: "30%" }}>
            <Typography>Всего КБЖУ:</Typography>
            <Stack direction="row" sx={{ marginTop: 2 }}>
              <Stack sx={{ marginLeft: 2 }}>
                <Typography>К:</Typography>
                <Typography>Б:</Typography>
                <Typography>Ж:</Typography>
                <Typography>У:</Typography>
              </Stack>
              <Stack sx={{ marginLeft: 4 }}>
                <Typography>{kbju["k"].toFixed(2)}</Typography>
                <Typography>{kbju["b"].toFixed(2)}</Typography>
                <Typography>{kbju["j"].toFixed(2)}</Typography>
                <Typography>{kbju["u"].toFixed(2)}</Typography>
              </Stack>
            </Stack>
            <StartButton
              sx={{ marginTop: "auto", height: 55 }}
              onClick={handleApplyMeal}
            >
              Подтвердить
            </StartButton>
          </Stack>
        </Stack>
      </Box>
    </Modal>
  );
};

export default FoodModal;
