// IAPManager.cs
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;          // The Unity Purchasing system.
    private static IExtensionProvider storeExtensionProvider; // The store-specific Purchasing subsystems.

    public static string kProductIDConsumable = "consumable"; // Example product ID for a consumable product.

    void Start()
    {
        if (storeController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(kProductIDConsumable, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return storeController != null && storeExtensionProvider != null;
    }

    public void BuyConsumable()
    {
        BuyProductID(kProductIDConsumable);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = storeController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product);
            }
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("IAP Initialization Failed: " + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("IAP Purchase Failed: " + product.definition.id + ", " + failureReason);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == kProductIDConsumable)
        {
            Debug.Log("Purchase Successful: " + args.purchasedProduct.definition.id);
            // Handle successful purchase logic here
        }
        return PurchaseProcessingResult.Complete;
    }
}
