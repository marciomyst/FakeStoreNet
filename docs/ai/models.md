# AI Models Analysis

This directory contains visual comparisons of AI models evaluated in Azure AI Foundry to automate ASP.NET Core code generation from DDD documentation via AI prompts with minimal human intervention.

## Evaluation Goal

Identify the best-performing AI model in Azure AI Foundry for generating high-quality, DDD-compliant code with minimal manual corrections.

---

## Quality vs. Cost” Graph (GPT Models)
![Quality vs. Cost” Graph (GPT Models) Graph](gpt-models.png "Quality vs. Cost” Graph (GPT Models) Graph")

**X-Axis (Cost)**  
Operational billing (e.g., cents per 1,000 tokens). Lower cost is more efficient.

**Y-Axis (Quality)**  
Coding quality index (higher is better).

### Plotted Models

- **o3** (dark blue circle): Quality ≈ 0.90, Cost ≈ 20  
- **o4-mini** (pink square): Quality ≈ 0.89, Cost ≈ 5  
- **o1** (light green diamond): Quality ≈ 0.88, Cost ≈ 30  
- **o3-mini** (purple triangle): Quality ≈ 0.87, Cost ≈ 10  
- **gpt-4.1** (dark green circle): Quality ≈ 0.84, Cost ≈ 5  
- **gpt-4.1-mini** (orange square): Quality ≈ 0.81, Cost ≈ 5  
- **gpt-4.5-preview** (light blue diamond): Quality ≈ 0.80, Cost ≈ 95  

### “Most Attractive” Quadrant

Models combining high quality (≥ 0.88) with low cost (≤ 30):

- **o4-mini** (Quality 0.89, Cost 5)  
- **o3** (Quality 0.90, Cost 20)  
- **o1** (Quality 0.88, Cost 30)  

In contrast, **gpt-4.5-preview** and **gpt-4.1-mini** have either high cost or lower quality, making them less cost-effective for large-scale code generation.

---

## “Quality vs. Cost” Graph (Non-GPT Models)
![Quality vs. Cost” Graph (Non-GPT Models) Graph](other-models.png "Quality vs. Cost” Graph (Non-GPT Models) Graph")

**X-Axis (Cost)**  
Cost per 1,000 tokens or equivalent credits.

**Y-Axis (Quality)**  
Coding quality index.

### Plotted Models

- **DeepSeek-R1** (blue circle): Quality ≈ 0.87, Cost ≈ 2.5  
- **DeepSeek-V3-0324** (pink square): Quality ≈ 0.75, Cost ≈ 2.0  
- **Llama-4-Maverick-17B-128E-Instruct-FP8** (cyan diamond): Quality ≈ 0.79, Cost ≈ 0.8  
- **mistral-medium-2505** (purple triangle): Quality ≈ 0.76, Cost ≈ 1.0  
- **Mistral-Large-2411** (green circle): Quality ≈ 0.74, Cost ≈ 3.0  

**Observations**:  
- **DeepSeek-R1** shows the highest quality among non-GPT models but with a relatively higher cost.  
- **Llama-4-Maverick** offers low cost but lower quality (~0.79).  
- **Mistral** variants exhibit both lower quality (0.74–0.76) and moderate cost, making them less competitive.

---


## Models Compared and Scores for Programing Only
![Quality vs. Cost” Graph (GPT Models) Graph](coding-models.png "Quality vs. Cost” Graph (GPT Models) Graph")
- **o4-mini** → 0.75 (highest score in the chart)  
- **o3-mini** → 0.74  
- **o3** → 0.73  
- **DeepSeek-R1** → 0.69  
- **gpt-4.5-preview** → 0.63  

---

## Key Conclusions

- **o4-mini** leads the coding quality ranking with a score of 0.75.  
- **o3-mini** (0.74) and **o3** (0.73) deliver very close results, but o4-mini’s absolute advantage (0.75 vs. 0.74) suggests greater robustness across varied coding scenarios (multiple styles, complex patterns).  
- **DeepSeek-R1** and **gpt-4.5-preview** rank lower, indicating that despite their strengths in other domains, they do not outperform the GPT mini variants in code generation tasks.

---

## Why Choose o4-mini for Your Project?

1. **Highest Code Quality**  
   - Scored 0.75 in direct programming benchmarks (generation, refactoring, bug fixes).

2. **Optimal Quality × Cost**  
   - Falls in the “Most Attractive” quadrant: low cost (~5) and high quality (~0.89).  
   - Compared to o3 (0.90 quality but cost ~20), o4-mini is 4× cheaper for nearly the same performance.

3. **Scalability in CI/CD and IDE Workflows**  
   - Minimal manual corrections and prompt engineering translate into budget savings and faster pipelines.

4. **Future-Proof for RAG and Embedding Pipelines**  
   - Its compact size and high performance support embedding-based agents and large-scale document processing without undue latency or cost.

These insights justify selecting **o4-mini** as the optimal model for automating FakeStoreNet implementation with AI.
