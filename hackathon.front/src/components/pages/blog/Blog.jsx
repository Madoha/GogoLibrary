
const BlogSection = () => {
    const blogs = [
        {
            id: 1,
            title: 'Флаги, знамена, полезная информация, часть I',
            image: 'src/assets/blogimg1.png',
        },
        {
            id: 2,
            title: 'Флаги, знамена, полезная информация, часть I',
            image: 'src/assets/blogimg2.png',
        },
        {
            id: 3,
            title: 'Флаги, знамена, полезная информация, часть I',
            image: 'src/assets/blogimg3.png',
        },
    ];

    return (
        <div className="blog-section">
            <div className="blog-wrapper">
                <h2>НАШ БЛОГ</h2>
                <div className="blog-cards">
                    {blogs.map((blog) => (
                        <div className="blog-card" key={blog.id}>
                            <img src={blog.image} alt={blog.title} className="blog-image" />
                            <div className="blog-content">
                                <p>{blog.title}</p>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default BlogSection;
