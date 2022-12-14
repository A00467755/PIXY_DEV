﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PIXY.Models;

public class ImagesCategoryView
{
    public List<ImageVM>? ImagesVM { get; set; }
    public SelectList? Categories { get; set; }
    public string? ImageCategory { get; set; }
    public string? SearchTags { get; set; }
}