import { Modal, Box, Grid2, Typography } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import StorageContext from "../../../../store/StorageContext";
import ProductCard from "./ProductCard";

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

const ProductModal = ({ open, productsInUse, handleClose, onProductClick }) => {
  const { products, fetchProducts } = useContext(StorageContext);
  const [productsFromStorage, setProductsFromStorage] = useState([]);
  const [isNothingProducts, setIsNothingProducts] = useState(false);

  useEffect(() => {
    if (products.length === 0) {
      const get = async () => {
        setProductsFromStorage(await fetchProducts());
      };
      get();
      return;
    }
    setProductsFromStorage(products);
  }, [products, productsFromStorage]);

  useEffect(() => {
    setIsNothingProducts(products.length === productsInUse.length);
  }, [productsInUse]);

  const handleClick = (id) => {
    const product = productsFromStorage.find((ex) => ex.id === id);
    console.log({ ...product, weight: defaultWeight });
    onProductClick({ ...product, weight: defaultWeight });
  };

  return (
    <Modal open={open} onClose={handleClose}>
      <Box sx={style}>
        {!isNothingProducts ? (
          <Grid2 container spacing={4} justifyContent="space-around">
            {productsFromStorage &&
              productsFromStorage.map((product) => {
                if (!productsInUse.includes(product.id)) {
                  return (
                    <Grid2 item="true" xs={12} sm={6} md={3} key={product.id}>
                      <ProductCard
                        id={product.id}
                        name={product.name}
                        calories={product.calories}
                        proteins={product.proteins}
                        fats={product.fats}
                        carbohydrates={product.carbohydrates}
                        imageUrl={product.imageUrl}
                        onClick={handleClick}
                      />
                    </Grid2>
                  );
                }
              })}
          </Grid2>
        ) : (
          <Typography>
            К сожалению, пока больше нет доступных продуктов
          </Typography>
        )}
      </Box>
    </Modal>
  );
};

export default ProductModal;
