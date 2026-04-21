<%@ Page Title="Glutathione Injections - TAMAZ Global" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Category" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <%-- Page-specific styles if needed --%>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Breadcrumb -->
    <section class="py-3 bg-[#f8fafc] border-b border-gray-200">
        <div class="max-w-7xl mx-auto px-4">
            <nav class="text-sm text-[#64748B] breadcrumb">
                <a href="Default.aspx" class="hover:text-[#B91C1C] transition-colors">Home</a>
                <span class="mx-2">/</span>
                <a href="Category.aspx" class="hover:text-[#B91C1C] transition-colors">Category</a>
                <span class="mx-2">/</span>
                <span class="text-[#0F172A] font-medium">Glutathione Injections</span>
            </nav>
        </div>
    </section>

    <!-- Main Listing Content -->
    <section class="py-5 bg-white category-listing-section">
        <div class="max-w-7xl mx-auto px-4">
            <div class="mb-8">
                <h1 class="text-3xl font-bold text-[#0F172A]">Glutathione Injections</h1>
                <p class="text-[#64748B] mt-2">Browse our premium collection of high-quality injections.</p>
            </div>

            <!-- Product Grid -->
            <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-3 md:gap-6 mb-12">

                <!-- Card 1 -->
                <div class="product-card">
                    <a href="Product.aspx">
                        <div class="product-image">
                            <span class="product-badge">Best Seller</span>
                            <button class="wishlist-btn">
                                <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                    <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
                                </svg>
                            </button>
                            <img src="assests/Images/old-product/1.jpg" alt="Product" />
                        </div>
                        <div class="product-info">
                            <h3 class="product-name">Miracle NAD-XEL NAD+ Injection Advanced NAD Plus Cellular Enhancement Therapy</h3>
                            <div class="product-price">
                                <span class="current-price"><i class="fa-solid fa-indian-rupee-sign pe-1 fs-16"></i>4800.00</span>
                            </div>
                            <button class="add-cart-btn">Add to Cart</button>
                        </div>
                    </a>
                </div>

                <!-- Card 2 -->
                <div class="product-card">
                    <div class="product-image">
                        <button class="wishlist-btn">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
                            </svg>
                        </button>
                        <img src="assests/Images/old-product/2.jpg" alt="Product" />
                    </div>
                    <div class="product-info">
                        <h3 class="product-name">Miracle White Royal Gold 120000mg Glutathione Softgels</h3>
                        <div class="product-price">
                            <span class="current-price"><i class="fa-solid fa-indian-rupee-sign pe-1 fs-16"></i>6500.00</span>
                        </div>
                        <button class="add-cart-btn">Add to Cart</button>
                    </div>
                </div>

                <!-- Card 3 -->
                <div class="product-card">
                    <div class="product-image">
                        <span class="product-badge">New</span>
                        <button class="wishlist-btn">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
                            </svg>
                        </button>
                        <img src="assests/Images/old-product/3.png" alt="Product" />
                    </div>
                    <div class="product-info">
                        <h3 class="product-name">Miracle Dermal Genesis NAD Plus Renewal Complex Glutathione Injection</h3>
                        <div class="product-price">
                            <span class="current-price">Contact Us</span>
                        </div>
                        <button class="add-cart-btn">Add to Cart</button>
                    </div>
                </div>

                <!-- Card 4 -->
                <div class="product-card">
                    <div class="product-image">
                        <button class="wishlist-btn">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
                            </svg>
                        </button>
                        <img src="assests/Images/old-product/4.jpg" alt="Product" />
                    </div>
                    <div class="product-info">
                        <h3 class="product-name">Neocell Super Collagen 3000mg Tablets with Vitamin C &amp; Biotin</h3>
                        <div class="product-price">
                            <span class="current-price"><i class="fa-solid fa-indian-rupee-sign pe-1 fs-16"></i>5500.00</span>
                        </div>
                        <button class="add-cart-btn">Add to Cart</button>
                    </div>
                </div>

                <!-- Card 5 -->
                <div class="product-card">
                    <div class="product-image">
                        <button class="wishlist-btn">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
                            </svg>
                        </button>
                        <img src="assests/Images/old-product/5.jpg" alt="Product" />
                    </div>
                    <div class="product-info">
                        <h3 class="product-name">Bio Rae Nano Complexion 18 Skin Whitening Glutathione Injection</h3>
                        <div class="product-price">
                            <span class="current-price"><i class="fa-solid fa-indian-rupee-sign pe-1 fs-16"></i>7800.00</span>
                        </div>
                        <button class="add-cart-btn">Add to Cart</button>
                    </div>
                </div>

                <!-- Card 6 -->
                <div class="product-card">
                    <div class="product-image">
                        <button class="wishlist-btn">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
                            </svg>
                        </button>
                        <img src="assests/Images/old-product/6.jpg" alt="Product" />
                    </div>
                    <div class="product-info">
                        <h3 class="product-name">Cindyrella Celebrity Drip with NAD Plus Glutathione Skin Whitening Injection</h3>
                        <div class="product-price">
                            <span class="current-price"><i class="fa-solid fa-indian-rupee-sign pe-1 fs-16"></i>4500.00</span>
                        </div>
                        <button class="add-cart-btn">Add to Cart</button>
                    </div>
                </div>

                <!-- Card 7 -->
                <div class="product-card">
                    <div class="product-image">
                        <button class="wishlist-btn">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
                            </svg>
                        </button>
                        <img src="assests/Images/old-product/7.png" alt="Product" />
                    </div>
                    <div class="product-info">
                        <h3 class="product-name">Luthione 1200mg Glutathione Injection In India</h3>
                        <div class="product-price">
                            <span class="current-price"><i class="fa-solid fa-indian-rupee-sign pe-1 fs-16"></i>6500.00</span>
                        </div>
                        <button class="add-cart-btn">Add to Cart</button>
                    </div>
                </div>

                <!-- Card 8 -->
                <div class="product-card">
                    <div class="product-image">
                        <button class="wishlist-btn">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                                <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
                            </svg>
                        </button>
                        <img src="assests/Images/old-product/8.jpg" alt="Product" />
                    </div>
                    <div class="product-info">
                        <h3 class="product-name">Miracle White 80,000mg Glutathione Injection White Box</h3>
                        <div class="product-price">
                            <span class="current-price"><i class="fa-solid fa-indian-rupee-sign pe-1 fs-16"></i>1999.00</span>
                        </div>
                        <button class="add-cart-btn">Add to Cart</button>
                    </div>
                </div>

            </div>

            <!-- Pagination -->
            <nav class="flex justify-center items-center gap-2 mb-16">
                <a href="#" class="pagination-btn pagination-prev">
                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                        <polyline points="15 18 9 12 15 6" />
                    </svg>
                    Prev
                </a>
                <div class="flex items-center gap-2">
                    <a href="#" class="pagination-num active">1</a>
                    <a href="#" class="pagination-num">2</a>
                    <a href="#" class="pagination-num">3</a>
                    <span class="pagination-dots">...</span>
                    <a href="#" class="pagination-num">12</a>
                </div>
                <a href="#" class="pagination-btn pagination-next">
                    Next
                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                        <polyline points="9 18 15 12 9 6" />
                    </svg>
                </a>
            </nav>
        </div>
    </section>

    <!-- Editor Content Section -->
    <section class="section-padding pt-60 border-t border-gray-200 pt-12">
        <div class="bg-transparent">
            <div class="editor-content max-w-7xl mx-auto px-4">
                <h2>Buy Glutathione Skin Whitening Injection In India At The Best Price</h2>
                <p>
                    Are you looking for a safe and effective way to achieve brighter, radiant skin? Your search ends here! Welcome to Tamaz Global, your trusted destination for high-quality Glutathione injections designed to enhance your skin's glow.
                </p>
                <p>
                    Skin whitening injections, also known as skin lightening injections, have gained immense popularity for their ability to promote an even skin tone. Glutathione, the "mother of all antioxidants," plays a vital role in maintaining healthy, youthful skin. Before opting for this treatment, let's explore everything you need to know!
                </p>
                <h3>What is Glutathione?</h3>
                <p>Glutathione is a powerful antioxidant naturally produced in the liver. It consists of three amino acids: cysteine, glutamic acid, and glycine. This compound is responsible for:</p>
                <ul>
                    <li>Detoxifying harmful toxins</li>
                    <li>Elimination of toxins and impurities</li>
                    <li>Increased collagen synthesis for youthful skin</li>
                </ul>
                <h3>Are Glutathione Injections Safe?</h3>
                <h3>Usage Instructions</h3>
                <ol>
                    <li>Consult with a healthcare professional.</li>
                    <li>Follow the dosage instructions provided.</li>
                    <li>Store in a cool, dry place.</li>
                </ol>
                <h2>Explore Our Best-Selling Glutathione &amp; Skin Whitening Products</h2>
                <h3>Best Glutathione Injections</h3>
                <ul>
                    <li><a href="#">Dr. James Glutathione Injection 1500mg</a></li>
                    <li><a href="#">Vesco Pharma Gluta C 1000 Injection</a></li>
                </ul>
                <p>For bulk orders, contact our wholesale team.</p>
            </div>
        </div>
    </section>

</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="scripts" runat="server">
    <%-- Category page scripts if needed --%>
</asp:Content>
