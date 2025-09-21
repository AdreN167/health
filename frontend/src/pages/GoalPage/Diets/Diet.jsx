import {
  Box,
  Button,
  IconButton,
  Stack,
  styled,
  Typography,
} from "@mui/material";
import DeleteForeverOutlinedIcon from "@mui/icons-material/DeleteForeverOutlined";
import { grey } from "@mui/material/colors";
import AddIcon from "@mui/icons-material/Add";
import UpdateButton from "../../../components/UpdateButton/UpdateButton";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import { useContext, useEffect, useState } from "react";
import DietsContext from "../../../store/Diets/DietsContext";
import FoodCard from "./Food/FoodCard";
import InnerShadowBox from "../../../components/InnerShadowBox/InnerShadowBox";
import ProductModal from "./Products/ProductModal";
import DishModal from "./Dishes/DishModal";
import FoodModal from "./Food/FoodModal";
import { getFormatedDate } from "../../../common/helpers";
import dayjs from "dayjs";
import { createDietEvent } from "../../../api/dietEventJournalService";
import StartButton from "../../../components/StartButton/StartButton";
import AuthContext from "../../../store/AuthContext";

const AddButton = styled(Button)(({ theme }) => ({
  color: grey[400],
  "&:hover": {
    color: grey[600],
  },
}));

const Diet = ({
  id,
  name,
  addedProducts,
  addedDishes,
  onUpdate,
  onDelete,
  disabled = false,
}) => {
  const { token } = useContext(AuthContext);

  const [openProductsModal, setOpenProductsModal] = useState(false);
  const [openDishesModal, setOpenDishesModal] = useState(false);
  const [openFoodModal, setOpenFoodModal] = useState(false);
  const [scrollPostion, setScrollPosition] = useState(0);

  const handleOpenProductsModal = () => {
    setScrollPosition(window.scrollY);
    setOpenProductsModal(true);
  };
  const handleCloseProductsModal = () => {
    setOpenProductsModal(false);
    window.scrollTo(0, scrollPostion);
  };

  const handleOpenDishesModal = () => {
    setScrollPosition(window.scrollY);
    setOpenDishesModal(true);
  };
  const handleCloseDishesModal = () => {
    setOpenDishesModal(false);
    window.scrollTo(0, scrollPostion);
  };

  const handleOpenFoodModal = () => {
    setScrollPosition(window.scrollY);
    setOpenFoodModal(true);
  };
  const handleCloseFoodModal = () => {
    setOpenFoodModal(false);
    window.scrollTo(0, scrollPostion);
  };

  const { addDishes, addProducts } = useContext(DietsContext);
  const [products, setProducts] = useState(addedProducts);
  const [dishes, setDishes] = useState(addedDishes);

  const handleClickDeleteDiet = () => {
    onDelete(id);
  };

  const handleClickUpdateDiet = () => {
    onUpdate(id);
  };

  const handleProductInModalClick = (product) => {
    const allProducts = [product, ...products];
    setProducts(allProducts);
    addProducts(id, allProducts); // заменят текущий список новым, а не добавляет к существующему
    setOpenProductsModal(false);
  };

  const handleDeleteProductClick = (prodId) => {
    const filteredProducts = products.filter((prod) => prod.id !== prodId);
    setProducts(filteredProducts);
    addProducts(id, filteredProducts);
  };

  const handleProductWeightChange = (prodId, weight) => {
    if (weight == "0" || weight === "") return;
    const allProducts = products.map((prod) =>
      prod.id === prodId
        ? {
            ...prod,
            calories: (weight * prod.calories) / prod.weight,
            fats: (weight * prod.fats) / prod.weight,
            proteins: (weight * prod.proteins) / prod.weight,
            carbohydrates: (weight * prod.carbohydrates) / prod.weight,
            weight: weight,
          }
        : prod
    );
    setProducts(allProducts);
    addProducts(id, allProducts);
  };

  const handleDishInModalClick = (dish) => {
    const allDishes = [dish, ...dishes];
    setDishes(allDishes);
    addDishes(id, allDishes); // заменят текущий список новым, а не добавляет к существующему
    setOpenDishesModal(false);
  };

  const handleDeleteDishClick = (dishId) => {
    const filteredDishes = dishes.filter((prod) => prod.id !== dishId);
    setDishes(filteredDishes);
    addDishes(id, filteredDishes);
  };

  const handleDishWeightChange = (dishId, weight) => {
    if (weight == "0" || weight === "") return;
    const allDishes = dishes.map((prod) =>
      prod.id === dishId
        ? {
            ...prod,
            calories: (weight * prod.calories) / prod.weight,
            fats: (weight * prod.fats) / prod.weight,
            proteins: (weight * prod.proteins) / prod.weight,
            carbohydrates: (weight * prod.carbohydrates) / prod.weight,
            weight: weight,
          }
        : prod
    );
    setDishes(allDishes);
    addDishes(id, allDishes);
  };

  const handleApplyMeal = async (productIdList, dishIdList) => {
    const body = {
      dietId: id,
      date: getFormatedDate(dayjs().toString()),
      productIds: productIdList,
      dishIds: dishIdList,
    };
    try {
      await createDietEvent(token, body);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <>
      <FoodModal
        open={openFoodModal}
        handleClose={handleCloseFoodModal}
        products={products}
        dishes={dishes}
        onApplyMeal={handleApplyMeal}
      />
      <ProductModal
        open={openProductsModal}
        handleClose={handleCloseProductsModal}
        productsInUse={products.map((ex) => ex.id)}
        onProductClick={handleProductInModalClick}
      />
      <DishModal
        open={openDishesModal}
        handleClose={handleCloseDishesModal}
        dishesInUse={dishes.map((ex) => ex.id)}
        onDishClick={handleDishInModalClick}
      />
      <Box sx={{ width: "100%" }}>
        <Stack direction="row" alignItems="center" sx={{ maxWidth: 250 }}>
          <Typography
            style={{
              maxWidth: 170,
              whiteSpace: "nowrap",
              overflow: "hidden",
              textOverflow: "ellipsis",
            }}
          >
            {name}
          </Typography>

          {!disabled ? (
            <Box marginLeft="auto">
              <IconButton onClick={handleClickDeleteDiet}>
                <DeleteForeverOutlinedIcon />
              </IconButton>
              <IconButton onClick={handleClickUpdateDiet}>
                <EditOutlinedIcon />
              </IconButton>
            </Box>
          ) : (
            <StartButton sx={{ marginLeft: 2 }} onClick={handleOpenFoodModal}>
              Прием
            </StartButton>
          )}
        </Stack>
        <Stack
          direction="row"
          sx={{
            paddingRight: 2,
          }}
        >
          {!disabled && (
            <>
              <Box sx={{ marginRight: 2, width: "50%" }}>
                <AddButton
                  sx={{ transition: 0.2 }}
                  variant="text"
                  startIcon={<AddIcon sx={{ transition: 0.2 }} />}
                  onClick={handleOpenProductsModal}
                >
                  Добавить продукт
                </AddButton>
              </Box>
              <Box sx={{ marginLeft: 2, width: "50%" }}>
                <AddButton
                  sx={{ transition: 0.2 }}
                  variant="text"
                  startIcon={<AddIcon sx={{ transition: 0.2 }} />}
                  onClick={handleOpenDishesModal}
                >
                  Добавить блюдо
                </AddButton>
              </Box>
            </>
          )}
        </Stack>
        <Stack
          direction="row"
          sx={{
            paddingRight: 2,
            height: 340,
          }}
        >
          <InnerShadowBox
            display="flex"
            flexDirection="row"
            gap={4}
            marginTop={2}
            sx={{
              width: "50%",
              marginRight: 2,
              borderRadius: 2,
            }}
          >
            {products.map((prod) => (
              <FoodCard
                disabled={disabled}
                key={prod.id}
                id={prod.id}
                name={prod.name}
                calories={prod.calories}
                proteins={prod.proteins}
                fats={prod.fats}
                carbohydrates={prod.carbohydrates}
                weight={Number(prod.weight)}
                imageUrl={prod.imageUrl}
                onClick={handleDeleteProductClick}
                onWeightChange={handleProductWeightChange}
              />
            ))}
          </InnerShadowBox>
          <InnerShadowBox
            display="flex"
            flexDirection="row"
            gap={4}
            marginTop={2}
            sx={{
              width: "50%",
              marginLeft: 2,
              borderRadius: 2,
            }}
          >
            {dishes.map((prod) => (
              <FoodCard
                disabled={disabled}
                key={prod.id}
                id={prod.id}
                name={prod.name}
                calories={prod.calories}
                proteins={prod.proteins}
                fats={prod.fats}
                carbohydrates={prod.carbohydrates}
                weight={Number(prod.weight)}
                imageUrl={prod.imageUrl}
                onClick={handleDeleteDishClick}
                onWeightChange={handleDishWeightChange}
              />
            ))}
          </InnerShadowBox>
        </Stack>
      </Box>
    </>
  );
};

export default Diet;
